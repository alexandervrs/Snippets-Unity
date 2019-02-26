
/**
 * Graphics.cs
 * Graphics & Low level Graphics Library (GL) related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Draw a Quad
----------------------------------------- */
/// Class Body:
Material drawMaterial;

/// Start():
//create and set a new material using the default unlit Sprite shader
drawMaterial = new Material(Shader.Find("Sprites/Default"));
drawMaterial.SetPass(0); // activate the shader pass for rendering

/// OnRenderObject(), OnRenderImage(), OnPostRender():
// size of quad (normalized to 0-1 for on Screen drawing, actual world coordinates when using localToWorldMatrix)
float x = 0f;
float y = 0f;
float width  = 1f;
float height = 1f;

// draw setup
GL.PushMatrix(); // saves both projection and modelview matrices to the matrix stack

GL.MultMatrix(transform.localToWorldMatrix); // draw on game's world
//GL.LoadOrtho(); // draw on screen

// draw begin
GL.Begin(GL.QUADS); // draw quad fill
//GL.Begin(GL.TRIANGLE_STRIP); // draw quad outline

GL.Color(Color.yellow); // set quad color

// draw quad vertices with Y axis flipped
GL.Vertex(new Vector3(x, 1-y));
GL.Vertex(new Vector3(x + width, 1-y));

GL.Vertex(new Vector3(x + width, 1-y));
GL.Vertex(new Vector3(x + width, 1-y - height));

GL.Vertex(new Vector3(x + width, 1-y - height));
GL.Vertex(new Vector3(x, 1-y - height));

GL.Vertex(new Vector3(x, 1-y - height));
GL.Vertex(new Vector3(x, 1-y));

// draw end
GL.End();
GL.PopMatrix(); // restores both projection and modelview matrices off the top of the matrix stack

/// OnDestroy():
Destroy(drawMaterial); // destroy the material when object is destroyed


/* -----------------------------------------
   Draw Mesh standalone
----------------------------------------- */
/// Class Body:
Mesh     mesh;
Material material;

/// OnRenderObject(), OnRenderImage(), OnPostRender():
Vector3    position = Vector3.zero;
Quaternion rotation = Quaternion.identity;
Vector3    scale    = new Vector3(6f, -6f, 6f);

// create 4x4 (translation, rotation, scale) transformation matrix
Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);

material.SetPass(0); // set the pass of the assigned material
Graphics.DrawMeshNow(mesh, matrix, 0); // draw the mesh, with assigned transformation matrix and material index pass 0 (like above)

