
/**
 * Application.cs
 * Application related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Change Quality Settings
----------------------------------------- */
// set AntiAlias
QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
QualitySettings.antiAliasing = 2;

// set VSync
QualitySettings.vSyncCount = 1;


/* -----------------------------------------
   Set Framerate
----------------------------------------- */
QualitySettings.vSyncCount = 0; // vSyncCount needs to be 0 for Application.targetFrameRate to work
Application.targetFrameRate = 60;


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

