
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
Shader "VFX/Sprite/Grayscale"
{ 
	// exposed properties
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Alpha("Alpha", Range (0, 1)) = 1.0
        _EffectAmount("Effect Amount", Range (0, 1)) = 1.0
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

        ZWrite Off Blend One OneMinusSrcAlpha Cull Off

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
            uniform float     _EffectAmount;
            uniform sampler2D _MainTex;
            uniform float     _Alpha;
            uniform float4    _Color;

            // grayscale filter
            float4 grayscale(float4 inColor, float amount)
            {
                float3 grayscaleColor = dot(inColor.rgb, float3(0.3, 0.59, 0.11));
                return lerp(inColor, float4(grayscaleColor, inColor.a), amount);

            }

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
                float4 textureColor = tex2D(_MainTex, i.texcoord);

                // grayscale effect
                float4 grayscaleOutput = grayscale(textureColor, _EffectAmount);
                grayscaleOutput.a = textureColor.a*_Alpha;

                // final output
                float4 finalOutput = grayscaleOutput;
                finalOutput.rgb   *= i.color.rgb;
                finalOutput.a      = finalOutput.a * _Alpha * i.color.a;
                finalOutput.rgb   *= finalOutput.a;
                finalOutput.a      = saturate(finalOutput.a);
                return finalOutput;

            }

            ENDCG

        }

    }

    Fallback "Sprites/Default"

}
// -------------( Grayscale.shader )--------------

// then attach Shader a Material, find shader under VFX > Sprite > Grayscale
// Shader exposes the Main texture, Alpha and EffectAmount to control the effect
// Set material to GameObject in order to apply the effect, either in Editor or runtime


// note: If you have a shader that affects the entire texture/sprite, remember to change its Mesh Type to "Full Rect"


/* -----------------------------------------
   GrabPass Shader Effect
----------------------------------------- */

// note: A GrabPass shader can be set as a Material on a Sprite or Quad (GameObject > 3D Object > Quad)

// first create the Shader, e.g. GrabPassGrayscale.shader

// -------------( GrabPassGrayscale.shader )--------------
Shader "VFX/GrabPass/Grayscale"
{ 
	// exposed properties
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}  // optional: allow SpriteRenderer texture
        _Alpha("Alpha", Range (0, 1)) = 1.0
        _EffectAmount("Effect Amount", Range (0, 1)) = 1.0
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "true"
            "RenderType" = "Opaque"
            "PreviewType" = "Plane"
        }

		// no ZWrite, Cull or Blend setup ...

        // do a GrabPass and store the data in _GrabTexture
        GrabPass { "_GrabTexture" } 

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
                float2 screencoord : TEXCOORD1;
                float4 vertex   : SV_POSITION;
                float4 color    : COLOR;
            };

			// uniforms
            uniform float _EffectAmount;
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex;
            uniform float _Alpha;

			// vertex shader
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                float4 screenpos = ComputeGrabScreenPos(OUT.vertex); // grab pass screen position
                OUT.screencoord = screenpos.xy / screenpos.w;
                OUT.texcoord = IN.texcoord; // optional: allow SpriteRenderer texture
                OUT.color = IN.color;
                return OUT;
            }

			// fragment shader
            float4 frag (v2f i) : COLOR
            {
                float4 c2 = tex2D(_MainTex, i.texcoord)*i.color;  // optional: allow SpriteRenderer texture
                
                float4 c = tex2D(_GrabTexture, i.screencoord)*i.color;
                c.rgb = lerp(c.rgb, dot(c.rgb, float3(0.3, 0.59, 0.11)), _EffectAmount);
                // c.a = c.a*_Alpha;
                c.a = c2.a*_Alpha; // optional: allow SpriteRenderer texture alpha
                return float4(c.rgb, c.a);
            }

            ENDCG

        }

    }

    Fallback "Sprites/Default"

}
// -------------( GrabPassGrayscale.shader )--------------

// then attach Shader a Material, find shader under VFX > GrabPass > Grayscale
// Shader exposes the Main texture (optional for smooth effects), Alpha and EffectAmount to control the effect
// Set material to GameObject in order to apply the effect, either in Editor or runtime
// GrabPass shaders affect the region that your Sprite or Quad occupies and are applied to anything under it (via Sorting Layer / Z order)


/* -----------------------------------------
   PostProcess Shaders to RenderTexture
----------------------------------------- */

// note: Shaders applied to RenderTexture need the following options, no Blending (since we assume fullscreen image):
//
//       ZWrite Off ZTest Always Cull Off
//
//       Apply the following to a Camera GameObject:
//

/* 

    Important note: Shaders used as Post Processing and not linked to a Material in the Hierarchy or Prefab are not going to be compiled 
                    or included in Asset Bundles.
					
					To reference and compile a Shader, do one of the following:
	
					* Drag and drop a Material asset that uses the Shader you want in a public variable field of a GameObject in the Scene
					* Use an invisible GameObject with a Sprite or Mesh Renderer that references the Material that uses the Shader you want
					* Include the shader in a ShaderVariantCollection and use Warmup(), if Asset Bundle references a Scene, the Shader and ShaderVariantCollection needs to be loaded from another AssetBundle, as Scene Asset Bundles cannot include any resources that the Scene is not referencing
					* Place the Shader inside a "Resources" folder, that will force compile the Shader but also cause it to be included in the main application, it is maybe not ideal if you want to include the Shader in an Asset Bundle

*/

/// Class Body:
// create materials for the shaders
Material grayscaleFilterMaterial;
Material blurFilterMaterial;

// filter amounts
float grayscaleFilterAmount = 1.0f;
float blurFilterAmount = 3.0f;

// uniform IDs
int uniformFXAmount;

/// Start():
// get materials for the postprocess shaders, we use "VFX/PostProcess/" to differentiate from Sprite-related shaders
grayscaleFilterMaterial = new Material( Shader.Find("VFX/PostProcess/Grayscale") );
blurFilterMaterial      = new Material( Shader.Find("VFX/PostProcess/Blur") );

// store the Shader uniforms as IDs
uniformFXAmount = Shader.PropertyToID("_EffectAmount");

/// OnRenderImage():
void OnRenderImage(RenderTexture source, RenderTexture destination)
{

    // demonstration of using 2 shaders at the same time:

    // create temporary RenderTextures for the shader effect passes
	RenderTexture temp  = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
	RenderTexture temp2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

    // clear the RenderTextures
    temp.DiscardContents();
    temp2.DiscardContents();

    // first shader pass, using Graphics.Blit() to copy the contents of source rendertexture to temp rendertexture after applying grayscaleFilterMaterial
    grayscaleFilterMaterial.SetFloat(uniformFXAmount, grayscaleFilterAmount);
	Graphics.Blit(source, temp, grayscaleFilterMaterial);

    // second shader pass, using Graphics.Blit() to copy the contents of temp rendertexture to temp2 rendertexture after applying blurFilterMaterial
    blurFilterMaterial.SetFloat(uniformFXAmount, blurFilterAmount);
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
   Change Material Shader on runtime
----------------------------------------- */

// note: remember to destroy the material copy

// find the shader of a material
Renderer rend = GetComponent<Renderer>(); // or SpriteRenderer for sprites
rend.material.shader = Shader.Find("VFX/Sprite/Grayscale");

// set Uniform value (Float)
rend.material.SetFloat("_EffectAmount", 1.0f);

// set main texture Sampler
Texture2D tex;
rend.material.SetTexture("_MainTex", tex);


/* -----------------------------------------
   Check Compatibility
----------------------------------------- */
/// Class Body:
Shader shaderGrayscalePost;

/// Start():
shaderGrayscalePost = Shader.Find("VFX/PostProcess/Grayscale");

// check if shader is supported on the GPU
if (!shaderGrayscalePost.isSupported) {
    // not supported, disable this shader effect etc ...
}


/* -----------------------------------------------
   Shader Variant Collections, preload Shaders
----------------------------------------------- */

/* 

	Shaders are being prepared the first time they are used, this can cause a performance hiccup
    Use ShaderVariantCollections to prewarm shaders before using them
    Also can be used to mark Shaders for compilation/inclusion in the app or AssetBundle

    1. Go to Assets > Create > Shader > Shader Variant Collection
    2. Add the shaders to the ShaderVariantCollection
    3. Use Warmup() to preload the Shaders

*/



/// Class Body:
ShaderVariantCollection variant;

/// Awake(), Start():
variant.Warmup();


/* -----------------------------------------------------------
   Use Checkbox for Shader Property/Uniform in Inspector
----------------------------------------------------------- */
/// Properties:
[Toggle(OPTION_COLORIZE_B)] _InvertAlpha ("Colorize Blue", Float) = 0

/// Pass:
#pragma multi_compile __ OPTION_COLORIZE_B

/// frag():
#ifdef OPTION_COLORIZE_B
c.rgb = c.rgb*float3(0, 2, 2);
/// ...
#endif


/* -----------------------------------------
   Support for UI Mask
----------------------------------------- */

/* 
	In order to support Mask Component for UI elements,
	the following must be added to the Shader:
*/

/// Properties:
// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15


/// Subshader:
// required for UI.Mask
Stencil
{
	Ref[_Stencil]
	Comp[_StencilComp]
	Pass[_StencilOp]
	ReadMask[_StencilReadMask]
	WriteMask[_StencilWriteMask]
}

