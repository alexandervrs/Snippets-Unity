
/**
 * Screen.cs
 * Screen related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Set Game Window Size & Fullscreen
----------------------------------------- */
// windowed with a window size of 1280 x 720
Screen.SetResolution(1280, 720, false);

// fullscreen to the display's resolution
Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);


/* -----------------------------------------
   Set allowed Screen Orientations
----------------------------------------- */
Screen.autorotateToLandscapeLeft      = true;
Screen.autorotateToLandscapeRight     = true;
Screen.autorotateToPortrait           = false;
Screen.autorotateToPortraitUpsideDown = false;


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
   Set Sleep/Powersaving Mode
----------------------------------------- */
// disable sleep mode
Screen.sleepTimeout = SleepTimeout.NeverSleep;

// default sleep mode
Screen.sleepTimeout = SleepTimeout.SystemSetting;


/* -----------------------------------------
   Get Info
----------------------------------------- */
// get display resolution size
int screenWidth  = Screen.currentResolution.width;
int screenHeight = Screen.currentResolution.height;

// get game window size
int windowWidth  = Screen.width;
int windowHeight = Screen.height;

// get screen DPI
float dpi = Screen.dpi;

// check if game window is fullscreen
if (Screen.fullScreen) {
	// is fullscreen ...
}

// check screen orientation (Landscape, LandscapeLeft, LandscapeRight, Portrait, PortraitUpsideDown)
if (Screen.orientation == ScreenOrientation.Landscape) {
	// is landscape ...
}


/* -----------------------------------------
   Take Screenshot
----------------------------------------- */
// save a screenshot of the game window in Appdata folder, tagged with datetime, 1 is superscaling (increase number for a larger screenshot)
ScreenCapture.CaptureScreenshot(Application.persistentDataPath+"\\Screenshot_"+System.DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss")+".png", 1);

// take a screenshot of the game window and use it as Texture
Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();

// take a screenshot of the game window and use it as a RenderTexture
RenderTexture renderTexture = new RenderTexture(1280, 720, 24);
ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
