
/**
 * Editor.PlayerSettings.cs
 * Editor PlayerSettings related snippets for Unity
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
   Player Settings (Editor)
----------------------------------------- */

// enable exlusive audio (Android & iOS)
// mutes all background audio when app is active
// note: requires READ_PHONE_STATE permission
PlayerSettings.muteOtherAudioSources = true;

// hide OS Statusbar (iOS Only)
PlayerSettings.statusBarHidden = true;

// disable Screen Rotation Animation (Android & iOS)
PlayerSettings.useAnimatedAutorotation = false;

// allow application in background
PlayerSettings.visibleInBackground = true;

