
/**
 * GL.cs
 * Low level Graphics Library related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Draw a Color Quad
----------------------------------------- */
/// Class Body:
Material quadMaterial;

/// Start():
//create a new material using the default Sprite shader
quadMaterial = new Material(Shader.Find("Sprites/Default"));

/// OnRenderObject(), OnPostRender():
GL.PushMatrix();
GL.LoadOrtho();

// set a material for the quad
quadMaterial.SetPass(0);

// draw a quad over whole screen
GL.Begin(GL.QUADS);
GL.Color(new Color32( 100, 149, 237, 255 ));
GL.Vertex3(0, 0, 0);
GL.Vertex3(1, 0, 0);
GL.Vertex3(1, 1, 0);
GL.Vertex3(0, 1, 0);
GL.End();

GL.PopMatrix();

/// OnDestroy():
Destroy(quadMaterial); // destroy the material when object is destroyed
