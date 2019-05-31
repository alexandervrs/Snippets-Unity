
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
   Editor Menu Items
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

// execute menu item
EditorApplication.ExecuteMenuItem("Tools/CustomItem");

// display menu item at position
EditorUtility.DisplayPopupMenu(new Rect(0, 32, 0, 0), "GameObject", null);

