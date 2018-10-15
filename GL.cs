
/**
 * GL.cs
 * Low level Graphics Library related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Draw a Color Quad
----------------------------------------- */
/// OnRenderObject(), OnPostRender():
void OnRenderObject()
{

    GL.PushMatrix();
    GL.LoadOrtho();

    //material.SetPass(0); set a material

    // draw a quad over whole screen
    GL.Begin(GL.QUADS);
    GL.Color(new Color32( 100, 149, 237, 255 ));
    GL.Vertex3(0, 0, 0);
    GL.Vertex3(1, 0, 0);
    GL.Vertex3(1, 1, 0);
    GL.Vertex3(0, 1, 0);
    GL.End();

    GL.PopMatrix();

}