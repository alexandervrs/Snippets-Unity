
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

// check if game window is fullscreen
if (Screen.fullScreen) {
	// is fullscreen ...
}


/* -----------------------------------------
   Take Screenshot
----------------------------------------- */
// save a screenshot of the game in Appdata folder, tagged with datetime, 1 is superscaling, increase number for a larger screenshot
ScreenCapture.CaptureScreenshot(Application.persistentDataPath+"\\Screenshot_"+System.DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss")+".png", 1);
