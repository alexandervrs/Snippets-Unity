
/**
 * Shader.cs
 * Shader related snippets for Unity
 */

/* using */
using UnityEngine;


/* 
	Important Note:
		
	Using "material" to change properties like color/alpha will cause Unity to create a copy of that material 
	so you can commit your changes without affecting other objects using the same material

*/

Material mMaterial;

void Start() 
{
	// get and store the material index
	mMaterial = GetComponent<Renderer>().material;
}

void OnDestroy()
{
	// destroy the material on object's Destroy event
	Destroy(mMaterial);
}
		
/*		
	You'd also need to use Resources.UnloadUnusedAssets() when done with the scene to remove those copies from memory if they exist

	Alternatively you could use sharedMaterial which does not need Resources.UnloadUnusedAssets() 
	but will also affect other objects' materials
*/


/* -----------------------------------------
   Create and apply Shader to Material
----------------------------------------- */

// first create the Shader, e.g. Grayscale.shader

// -------------( Grayscale.shader )--------------
Shader "VisualFX/Sprite/Grayscale"
{ 
	// exposed properties
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Alpha("Alpha", Range (0, 1)) = 1.0
        _Color("Color", Color) = (1, 1, 1, 1)
        _FXAmount("FXAmount", Range (0, 1)) = 1.0
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "true"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

        Pass
        {

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex   : SV_POSITION;
                float4 color    : COLOR;
            };

			// uniforms
            uniform float _FXAmount;
            sampler2D _MainTex;
			
            float _Alpha;
            float4 _Color;

			// vertex shader
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                return OUT;
            }

			// fragment shader
            float4 frag (v2f i) : COLOR
            {
                float4 c = tex2D(_MainTex, i.texcoord)*i.color;
                c.rgb = lerp(c.rgb, dot(c.rgb, float3(0.3, 0.59, 0.11)), _FXAmount); // GLSL mix() is lerp() in HLSL
                c.a = c.a*_Alpha;
                return float4(c.rgb, c.a);
            }

            ENDCG

        }

    }

    Fallback "Sprites/Default"

}
// -------------( Grayscale.shader )--------------

// then attach Shader a Material, find shader under VisualFX > Sprite > Grayscale
// Shader exposes the Main texture, Alpha and FXAmount to control the effect
// Set material to GameObject in order to apply the effect, either in Editor or runtime


// note: If you have a shader that affects the entire texture/sprite, remember to change its Mesh Type to "Full Rect"


/* -----------------------------------------
   Change Material Shader on runtime
----------------------------------------- */

// note: remember to destroy the material copy

// find the shader of a material
Renderer rend = GetComponent<Renderer>(); // or SpriteRenderer for sprites
rend.material.shader = Shader.Find("VisualFX/Sprite/Grayscale");

// set Uniform value (Float)
rend.material.SetFloat("_FXAmount", 1.0f);

// set main texture Sampler
Texture2D tex;
rend.material.SetTexture("_MainTex", tex);


/* -----------------------------------------
   PostProcess Shaders to RenderTexture
----------------------------------------- */

// note: Shaders applied to RenderTexture need the following options, no Blending (since we assume fullscreen image):
//
//       ZWrite Off ZTest Always Cull Off
//
//       Apply the following to a Camera GameObject:
//

/// Class Body:
// create materials for the shaders
Material grayscaleFilterMaterial;
Material blurFilterMaterial;

// uniform IDs
int uniformFXAmount;

/// Start():
// get materials for the postprocess shaders, we use "VisualFX/PostProcess/" to differentiate from Sprite-related shaders
grayscaleFilterMaterial = new Material( Shader.Find("VisualFX/PostProcess/Grayscale") );
blurFilterMaterial      = new Material( Shader.Find("VisualFX/PostProcess/Blur") );

// store the Shader uniforms as IDs
int uniformFXAmount = Shader.PropertyToID("_FXAmount");

/// OnRenderImage():
void OnRenderImage(RenderTexture source, RenderTexture destination)
{

    // demonstration of using 2 shaders at the same time:

    // create temporary RenderTextures for the shader effect passes
	RenderTexture temp  = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
	RenderTexture temp2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

    // first shader pass, using Graphics.Blit() to copy the contents of source rendertexture to temp rendertexture after applying grayscaleFilterMaterial
    grayscaleFilterMaterial.SetFloat(uniformFXAmount, grayscaleFilter);
	Graphics.Blit(source, temp, grayscaleFilterMaterial);

    // second shader pass, using Graphics.Blit() to copy the contents of temp rendertexture to temp2 rendertexture after applying blurFilterMaterial
    blurFilterMaterial.SetFloat(uniformFXAmount, blurFilter);
	Graphics.Blit(temp, temp2, blurFilterMaterial);

    // more shader passes can go here (needs extra temp RenderTextures) ...

    // render all to final RenderTexture and on camera
	Graphics.Blit(temp2, destination);

    // cleanup, release temporary RenderTextures from memory
    RenderTexture.ReleaseTemporary(temp);
	RenderTexture.ReleaseTemporary(temp2);

}

/// OnDestroy():
// remember to cleanup the materials
Object.Destroy(grayscaleFilterMaterial);
Object.Destroy(blurFilterMaterial);


/* -----------------------------------------
   Set Uniform Values
----------------------------------------- */

// set Uniform value (Float)
shaderMaterial.SetFloat("_UniformName", 1.0f);

// set Uniform value (Int)
shaderMaterial.SetInt("_UniformName", 1);

// set Uniform value (Vector)
shaderMaterial.SetVector("_UniformName", new Vector4(0.4f, 0.4f, 0.4f, 0.4f));


/* -----------------------------------------
   Check Compatibility
----------------------------------------- */
/// Class Body:
Shader shaderGrayscalePost;

/// Start():
shaderGrayscalePost = Shader.Find("VisualFX/PostProcess/Grayscale");

// check if shader is supported on the GPU
if (!shaderGrayscalePost.isSupported) {
    // not supported, disable this shader effect etc ...
}


/* -----------------------------------------
   Preload all Shaders
----------------------------------------- */
// fully load all shaders to prevent future performance hiccups
Shader.WarmupAllShaders();

