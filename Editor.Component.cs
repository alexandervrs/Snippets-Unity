
/**
 * Editor.Component.cs
 * Editor GameObject Component related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Components
----------------------------------------- */
// copy component values
ComponentUtility.CopyComponent(targetComponent); 

// paste component values
ComponentUtility.PasteComponentValues(targetComponent);

// duplicate component
System.Type type = original.GetType(); // original is Component
Component copy   = destination.AddComponent(type);  // destination is of type GameObject
EditorUtility.CopySerialized(original, copy);

// remove a component
DestroyImmediate(targetComponent);
GUIUtility.ExitGUI(); // exit the OnGUI() event after removing a component to avoid GUI Drawing errors

