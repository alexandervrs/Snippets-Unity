
/**
 * Plugin.cs
 * External API, Library related snippets for Unity
 */

/* using */
using System.Runtime.InteropServices; // for DllImport

/*
	Note: External libraries must be inside the Unity "Plugins" folder
		  32-bit plugins go under a x86 subfolder and 64bit under a x64 subfolder
*/


/* -----------------------------------------
   Call Plugin (Windows)
----------------------------------------- */
// Constructor:

// DLLs are only for the Windows platform
#if UNITY_STANDALONE_WIN

// Unity cannot give back the Window Handle (hWnd), so we need to call WINAPI
[DllImport("user32.dll")]
public static extern System.IntPtr GetActiveWindow();

[DllImport("WindowPlugin")] // load WindowPlugin.dll from "Plugins" folder
public static extern float MyPluginFunction(IntPtr hWnd); // call WindowPlugin which receives a hWnd IntPtr parameter and returns a float


/// Awake(), Start(), Update():

// call the function and pass the active window passed from WINAPI
MyPluginFunction( GetActiveWindow() );

#endif