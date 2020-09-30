
/**
 * HandheldDevice.cs
 * Handheld Device-specific related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Open Device Keyboard (Android & iOS)
----------------------------------------- */
///Class Body:
private TouchScreenKeyboard deviceKeyboard;
private string textToEdit = "";

/// Start(), Update():

// show the keyboard on screen
deviceKeyboard = TouchScreenKeyboard.Open(
	textToEdit,                       // output text
	TouchScreenKeyboardType.Default,  // keyboard type, Default, NamePhonePad, NumberPad, EmailAddress
	false,                            // autocorrect
	false,                            // multiline
	true,                             // secure
	false,                            // alert mode
	"",                               // text placeholder
	0                                 // character limit, 0 = infinite
);

/// Update():

// check status of the keyboard
switch (deviceKeyboard.status) {

	case TouchScreenKeyboard.Status.Done:
		// textToEdit variable now has the new value
		break;

	case TouchScreenKeyboard.Status.Canceled:
		textToEdit = "";
		// set variable textToEdit to nothing
		break;
	
	case TouchScreenKeyboard.Status.LostFocus:
		// keyboard focus was lost
		break;

}


/* -----------------------------------------
   Vibrate the Device (Android & iOS)
----------------------------------------- */
// vibrate the device
#if UNITY_ANDROID || UNITY_IOS
if (SystemInfo.supportsVibration) {
	Handheld.Vibrate();
}
#endif