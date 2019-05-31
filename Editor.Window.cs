
/**
 * Editor.Window.cs
 * Editor Window related snippets for Unity
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
   Editor Window
----------------------------------------- */

// shows a CustomEditor in a Window instead of a Component/Inspector

// -------------( SampleWindowEditor.cs )--------------

using UnityEditor;
using UnityEngine;

public class SampleWindowEditor : EditorWindow
{
    [MenuItem("Tools/Open Custom Window...")]
    static void OpenCustomEditor()
    {

        SampleWindowEditor window = (SampleWindowEditor)GetWindow(typeof(SampleWindowEditor));
		
        window.position     = new Rect(6, 100, 320, 200);    // initial window position
		window.titleContent = new GUIContent("Test Window"); // window title

		// set window icon (optional)
		Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Gizmos/SampleWindowEditor Icon.png"); // load window icon
		window.titleContent = new GUIContent("Test Window", icon); // window title & icon

		// handle resizing, same sizes for min and max size will disable resizing of the window
		window.minSize = new Vector2(320, 200);
        window.maxSize = new Vector2(320, 200);
		
		window.Show();	// show the window
		window.Focus(); // keyboard focus the window

		// OR create a persistent popout window
		SampleWindowEditor window2 = ScriptableObject.CreateInstance(typeof(SampleWindowEditor)) as SampleWindowEditor;
        window2.ShowUtility(); // show popout 
		
		// OR create a non persistent popout window
		SampleWindowEditor window2 = ScriptableObject.CreateInstance(typeof(SampleWindowEditor)) as SampleWindowEditor;
        window2.ShowAuxWindow(); // show popout (autocloses when user clicks outside the window) 
		
    }

    void OnGUI()
    {

		// window contents
		// ...
		
		// update window contents
		this.Repaint();
	
		if (GUILayout.Button("Close")) {
			this.Close(); // close the window
		}

	}

}

// -------------( SampleWindowEditor.cs )--------------


/* -----------------------------------------
   Launch Window
----------------------------------------- */

// launch Build Settings
EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
