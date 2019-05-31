
/**
 * Editor.Application.cs
 * Editor Application related snippets for Unity
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
   Game Playback
----------------------------------------- */

// stop game playback
EditorApplication.isPlaying = false;

// test run the game
EditorApplication.isPlaying = true;


/* -----------------------------------------
   Callbacks
----------------------------------------- */
// delegate which is called once after all inspectors update
EditorApplication.delayCall += () => {
			
	Debug.Log("Delayed Call");

};

/* -----------------------------------------
   Quit Editor
----------------------------------------- */
// save & exit
EditorApplication.SaveScene(EditorApplication.currentScene);
EditorApplication.Exit(0);

// exit without saving
EditorApplication.Exit(0);


/* -----------------------------------------
   Navigate to URL
----------------------------------------- */
Application.OpenURL("https://google.com");
