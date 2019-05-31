
/**
 * Editor.CustomEditor.cs
 * Editor CustomEditor related snippets for Unity
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
   Inspector Custom Editor
----------------------------------------- */

// -------------( SampleCustomEditor.cs )--------------

using UnityEngine;
using UnityEditor;

// note: change MyComponent to an existing Component type e.g. SpriteRenderer or custom Script (e.g. SampleCustomComponent)

[CustomEditor(typeof(SampleCustomComponent))]
public class SampleCustomEditor : Editor
{
	
	public override void OnInspectorGUI()
    {

		// set the component target that the additional GUI elements will appear to
		SampleCustomComponent targetComponent = (SampleCustomComponent)target;
		
		// draws the default Inspector items
		DrawDefaultInspector();
		
		// like DrawDefaultInspector() but doesn't draw the elements you tell it to
		// DrawPropertiesExcluding(serializedObject, "m_Script");

		if (GUILayout.Button("Test")) {
			//targetComponent.MyMethod(); // call a "MyMethod()" from targetComponent
			Debug.Log("Button Clicked...");
		}

	}

}

// -------------( SampleCustomEditor.cs )--------------

// target the specific Component Inspector
SampleCustomComponent targetComponent = (SampleCustomComponent)target;

// draws all the current Inspector items taken from the Component
DrawDefaultInspector();


/* --------------------------------------------
   Get Data from Default Inspector Controls
-------------------------------------------- */

/* 		note: The following is the way to show UI controls from the Default Inspector, in
			  in case you're not using DrawDefaultInspector();
*/

/* serialize the Inspector public variables */
/// Class Body:
private SerializedObject m_object;

/// OnEnable():
m_object = new SerializedObject(targets);

/// OnInspectorGUI():
// update the Inspector properties/variables
m_object.ApplyModifiedProperties();

// add a UI control based on the variable type
EditorGUILayout.PropertyField(m_object.FindProperty("somePublicVariable"));


/* --------------------------------------------
   Get Inspector Info
-------------------------------------------- */
// get Inspector current width
float inspectorWidth = EditorGUIUtility.currentViewWidth;

