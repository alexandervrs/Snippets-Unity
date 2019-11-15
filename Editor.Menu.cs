
/**
 * Editor.Menu.cs
 * Editor Menu related snippets for Unity
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
   Menu Item
----------------------------------------- */
// adds a menu item for the Editor
// hotkeys can be added with special modifier keys % (ctrl on Windows, cmd on macOS), # (shift), & (alt)

#if UNITY_EDITOR
public class EditorShortCutKeys : ScriptableObject {
	
	[MenuItem("Tools/CustomItem _F5", priority = int.MaxValue)] // priority is optional
	static void EditorHotkeyTestPlay() {
		// MainMenu > Tools > CustomItem was pressed, or F5 key was pressed
		Debug.Log("Menu Item selected...");
	}
	
	/// more ...
}
#endif


/* -----------------------------------------
   Menu Item with Validation
----------------------------------------- */

// validate condition for enabling the menu item
[MenuItem("Tools/Validate Asset", true)] // "true" denotes this method is used to validate the menu item condition before executing the actual menu action
private static bool MyMenuItem() {
	// validate selected asset is texture, audio and only one asset is selected
	return (Selection.activeObject.GetType() == typeof(Texture2D) || Selection.activeObject.GetType() == typeof(AudioClip)) && (Selection.objects.GetLength(0) < 2);
}

// if condition above validates ok then this method will be executed next
[MenuItem("Tools/Validate Asset")]
private static bool MyMenuItem() {
	Debug.Log("Menu Item selected...");
}


/* -----------------------------------------
   Menu Item manipulation
----------------------------------------- */

// execute menu item
EditorApplication.ExecuteMenuItem("Tools/CustomItem");

// display menu item at position
EditorUtility.DisplayPopupMenu(new Rect(0, 32, 0, 0), "GameObject", null);
