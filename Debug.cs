
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

// write a formatted console message (also works with Debug.LogWarningFormat(), Debug.LogErrorFormat())
Debug.LogFormat("Data Path is: <color=#f4f400>{0}</color>", Application.dataPath);


/* -----------------------------------------
   Logger Functionality
----------------------------------------- */
// disable all Debug logging (can make the game run faster)
ILogger logger = Debug.unityLogger;
logger.logEnabled = false;

// filter out to show only specific debug messages
ILogger logger = Debug.unityLogger;
logger.filterLogType = LogType.Error; // only show Errors


/* -----------------------------------------
   Compiler Warnings
----------------------------------------- */
// disable compiler warning 0219 (unused variable warning)
#pragma warning disable 0219
MeshRenderer renderer = target as MeshRenderer; // ...
#pragma warning restore 0219


/* -----------------------------------------
   Retrieve FPS
----------------------------------------- */
/*

	note: Save the following as a script named DebugFPS & attach it to a GameObject to display FPS on screen

*/

// -------------( DebugFPS.cs )--------------

using UnityEngine;

public class DebugFPS : MonoBehaviour {

	private string fps   = "";
	private float  delta = 0.0f;
	private float  msec  = 0.0f;

	void Update()
	{
		delta  += (Time.unscaledDeltaTime - delta) * 0.1f;
		msec   = Time.unscaledDeltaTime * 1000f;
		fps    = Mathf.Round(1.0f / delta).ToString()+" fps, "+Mathf.Round(msec).ToString()+" msec";
	}

	void OnGUI() {

		GUIStyle style = new GUIStyle();
		Color    color = new Color();

     	ColorUtility.TryParseHtmlString("#db397f", out color);
		style.normal.textColor = color;

		Rect rect = new Rect(0, 0, Screen.width-6, Screen.height-6 * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize  = Screen.height * 2 / 100;

		GUI.Label(rect, fps, style);
		
	}

}

// -------------( DebugFPSDisplay.cs )--------------


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

