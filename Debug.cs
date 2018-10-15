
/**
 * Debug.cs
 * Debugging related snippets for Unity
 */

/* using */
using UnityEngine;
using System; // for Type
using System.Reflection; // for Assembly, MethodInfo


/* -----------------------------------------
   Write info to the Console
----------------------------------------- */
// write a console message
Debug.Log("Debug Message!");

// only write a console message when game is a debug build
if (Debug.isDebugBuild) {
	Debug.Log("Debug Message!");
}

// write a console warning
Debug.LogWarning("Warning!");

// write a console error
Debug.LogError("Error!");


/* -----------------------------------------
   Compiler Warnings
----------------------------------------- */
// disable compiler warning 0219 (unused variable warning)
#pragma warning disable 0219
MeshRenderer renderer = target as MeshRenderer; // ...
#pragma warning restore 0219


/* -----------------------------------------
   Draw Rectangle Bounds
----------------------------------------- */
/// Class Body:

// draws a freeform rectangle bounding box
void DebugDrawRectangleColor(Vector3 position, Vector2 size, Color color) {

    Debug.DrawLine(position, new Vector3(position.x + size.x, position.y, position.z), color);
    Debug.DrawLine(position, new Vector3(position.x, position.y - size.y, position.z), color);
    Debug.DrawLine(new Vector3(position.x, position.y - size.y, position.z), new Vector3(position.x + size.x, position.y - size.y, position.z), color);
    Debug.DrawLine(new Vector3(position.x + size.x, position.y - size.y, position.z), new Vector3(position.x + size.x, position.y, position.z), color);

}

/// Update():
DebugDrawRectangleColor(transform.position, 1f, Color.yellow);


/* -----------------------------------------
   Draw Sprite Bounds
----------------------------------------- */
/// Class Body:

// draws a bounding box of the gameObject's assigned sprite taking care of pivot offset & scale
void DebugDrawBbox(GameObject go, Color color)
{

	SpriteRenderer sprRenderer = go.GetComponent<SpriteRenderer>();
	Vector3 position           = go.transform.position;
	Vector3 scale              = go.transform.localScale;

	Vector2 anchor = new Vector2( 
							(sprRenderer.sprite.pivot.x/sprRenderer.sprite.pixelsPerUnit)*scale.x,
							(sprRenderer.sprite.pivot.y/sprRenderer.sprite.pixelsPerUnit)*scale.y
						);

	Vector2 size = new Vector2(
							(sprRenderer.sprite.texture.width/sprRenderer.sprite.pixelsPerUnit)*scale.x,
							(sprRenderer.sprite.texture.height/sprRenderer.sprite.pixelsPerUnit)*scale.y
						);

	

	Debug.DrawLine(new Vector3(position.x-anchor.x, position.y-anchor.y, position.z),                   new Vector3(position.x-anchor.x + size.x, position.y-anchor.y, position.z), color);
	Debug.DrawLine(new Vector3(position.x-anchor.x, position.y-anchor.y, position.z),                   new Vector3(position.x-anchor.x, position.y-anchor.y+size.y, position.z), color);
	Debug.DrawLine(new Vector3(position.x-anchor.x, position.y-anchor.y+size.y, position.z),            new Vector3(position.x-anchor.x + size.x, position.y-anchor.y+size.y, position.z), color);
	Debug.DrawLine(new Vector3(position.x-anchor.x+size.x, position.y-anchor.y+size.y, position.z),     new Vector3(position.x-anchor.x+size.x, position.y-anchor.y, position.z), color);

}

/// Update():
DebugDrawBbox(this.gameObject, Color.yellow);


/* -----------------------------------------
    Debug Events
----------------------------------------- */

/* note: drawn only in the Editor Scene view for debugging */

// draw continuously
void OnDrawGizmos() {

    Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

}

// draw continuously only when GameObject is selected
void OnDrawGizmosSelected() {

    Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

}

