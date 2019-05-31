
/**
 * Editor.SceneView.cs
 * Editor SceneView and Events related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;
using System.Reflection; // for MethodInfo, BindingFlags only


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Listen to OnSceneGUI() Event
----------------------------------------- */
/// OnEnable():
SceneView.duringSceneGui -= OnSceneGUI;
SceneView.duringSceneGui += OnSceneGUI;

void OnSceneGUI(SceneView view) {
	
	Debug.Log("OnSceneGUI()");
	/// ...
}


/* -----------------------------------------
   Handle Mouse Events (OnSceneGUI())
----------------------------------------- */
// listen for continuous mouse events
view.wantsMouseMove = true;
view.wantsMouseEnterLeaveWindow = true;


/* -----------------------------------------
   Handle Events (OnSceneGUI())
----------------------------------------- */
// check if key down
// note: just use Event.current.control, Event.current.alt, Event.current.shift for special modifier keys
if (Event.current.isKey) {

	if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape) {
		Debug.Log("Escape was pressed");
	}

}

// check if mouse down
if (Event.current.isMouse) {

	if (Event.current.type == EventType.MouseDown) {
		Debug.Log("Mouse down");
	}

}

// check if mouse dragging
if (Event.current.isMouse) {

	if (Event.current.type == EventType.MouseDrag) {
		Debug.Log("Mouse dragging");
	}
	
}

// check if mousewheel up or down
if (Event.current.isScrollWheel) {

	if (Event.current.type == EventType.ScrollWheel && Event.current.delta.y < 0) {
		Debug.Log("Mousewheel up");
	}
	if (Event.current.type == EventType.ScrollWheel && Event.current.delta.y > 0) {
		Debug.Log("Mousewheel down");
	}

}

// consume the current event, prevent subsequent GUI from using it
Event.current.Use();


/* -----------------------------------------
   Draw GUI Elements (OnSceneGUI())
----------------------------------------- */
// find mouse position in Scene View relative to the Unity Editor GUI mouse position
Vector3 mousePositionInSceneView = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;

// add a default control
int control = GUIUtility.GetControlID(FocusType.Passive);
HandleUtility.AddDefaultControl(control);

// show GUI in SceneView with mouse position information
Handles.BeginGUI();

GUILayout.BeginArea(new Rect(4f, 4f, 300f, EditorGUIUtility.singleLineHeight * 3f));

Rect rect = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
GUI.Label(rect, "X: " + mousePositionInSceneView.x.ToString("0.00"), EditorStyles.whiteLabel);
rect = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
rect.y -= 4f;
GUI.Label(rect, "Y: " + mousePositionInSceneView.y.ToString("0.00"), EditorStyles.whiteLabel);
rect = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
rect.y -= 8f;
GUI.Label(rect, "Z: " + mousePositionInSceneView.z.ToString("0.00"), EditorStyles.whiteLabel);

GUILayout.EndArea();

Handles.EndGUI();

HandleUtility.Repaint();


/* -----------------------------------------
   Reset Game View
----------------------------------------- */
MethodInfo info = SceneView.lastActiveSceneView.GetType().GetMethod("OnNewProjectLayoutWasCreated", BindingFlags.Instance | BindingFlags.NonPublic);
info.Invoke(SceneView.lastActiveSceneView, null);

