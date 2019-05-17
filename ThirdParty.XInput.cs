
/**
 * XInput.cs
 * XInput related snippets for Unity
 */

/* using */
using UnityEngine;
using XInputDotNetPure;


/*
	Note: XInputDotNetPure needs its DLLs inside a "Plugins" folder
		  https://github.com/speps/XInputDotNet/releases
*/


/* -----------------------------------------
   Vibrate the Gamepad
----------------------------------------- */

// vibrate the gamepad (can be done in LateUpdate())
GamePad.SetVibration(PlayerIndex.One, 2.0f, 3.0f);


// stop vibration on Application exit
void OnApplicationQuit()
{
	GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
}