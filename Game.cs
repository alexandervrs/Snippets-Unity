
/**
 * Game.cs
 * Game related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Set Framerate
----------------------------------------- */
Application.targetFrameRate = 60;


/* -----------------------------------------
   Change Quality Settings
----------------------------------------- */
// set AntiAlias
QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
QualitySettings.antiAliasing = 2;

// set VSync
QualitySettings.vSyncCount = 1;


/* -----------------------------------------
   Switch Fullscreen mode with Alt+F4
----------------------------------------- */
// Constructor:

// (Windows Only) patch to fix Unity's lack of centering when switching to windowed mode
#if UNITY_STANDALONE_WIN

[System.Runtime.InteropServices.DllImport("user32.dll")]
public static extern System.IntPtr GetActiveWindow();

[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetWindowPos")]
private static extern bool SetWindowPos(System.IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

private int windowedModeWidth  = 1280;
private int windowedModeHeight = 720;

#endif

private void CenterWindow() {

	// use #if !UNITY_EDITOR to prevent centering the Editor instead
	#if !UNITY_EDITOR

	#if UNITY_STANDALONE_WIN

	int x = ((Screen.currentResolution.width - windowedModeWidth)   / 2);
	int y = ((Screen.currentResolution.height - windowedModeHeight) / 2);

	SetWindowPos(GetActiveWindow(), 0, x, y, windowedModeWidth, windowedModeHeight, 0);

	#endif

	#endif

}


// Update:
// switch fullscreen using the ALT+ENTER hotkey
if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return)) {

	Resolution res = Screen.currentResolution;

	if (Screen.fullScreen == true) {
		Screen.SetResolution(windowedModeWidth, windowedModeHeight, false);
		Cursor.visible = true;
		Invoke("CenterWindow", 0.1f);
	} else {
		Screen.SetResolution(res.width, res.height, true);
		Cursor.visible = false;
	}

}



/* -----------------------------------------
   Set Mouse Cursor Visibility
----------------------------------------- */
// hide the mouse cursor
Cursor.visible = false;


/* -----------------------------------------
   Retrieve Special folders
----------------------------------------- */
// get the game's Appdata folder, suitable for persistent data (savegames, cache files, user config)
string localStorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;

// get the game's Assets folder, requires assets to be inside a "StreamingAssets" root folder in the project Assets
// which will be copied over to the target's filesystem
string assetDirectory = Application.streamingAssetsPath + Path.DirectorySeparatorChar;

// get the game's Data folder (not suitable to load files from in some platforms (e.g. Android/iOS), use StreamingAssets there)
string dataDirectory = Application.dataPath + Path.DirectorySeparatorChar;


/* -----------------------------------------
   Quit Game
----------------------------------------- */
// quit game on ESC key press
if (Input.GetKeyDown(KeyCode.Escape)) {
	Application.Quit();
}


/* -----------------------------------------
   Navigate to URL
----------------------------------------- */
Application.OpenURL("https://google.com");

