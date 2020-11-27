
/**
 * InputSystem.cs
 * New Input System package related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.InputSystem;


/* -----------------------------------------
   Setup New Input System
----------------------------------------- */

/*

	a. Go to Window > Package Manager, find and install "Input System" package
	b. Next to Edit > Project Settings > Player, find "Active Input Handling" and set it to "Input System Package"

*/


/* -----------------------------------------
   Abstract Device Control
----------------------------------------- */

/*

	The new Input System abstracts device button presses, key presses, touches, clicks into Actions

	note: This is not 100% required, you can always use direct device control (see further below)
	      but Actions save time handling multiple types of input devices at the same time

	a. First go to Assets > Create > Input Actions
	b. Name the new asset to anything you like e.g. "PlayerControls" and click on it
	c. Create a new Action Map, named it anything you like e.g. "Player1"
	d. Choose the new Action Map "Player1" and click the + button to create a new Action
	e. Name it e.g. "MoveLeft" and click the + button next to it to "Add Binding"
	f. Choose a key/button from the list, e.g. "Left Arrow [Keyboard]"
	g. Add more Actions & Bindings as you see fit, Save and Close the window
	h. With the asset selected still, from the Inspector, check the option "Generate C# Class" and Apply
	i. Now you're ready to access those Actions from your own Scripts

*/

// Class Body:
public PlayerControls controls; // "PlayerControls" is the name that you gave the Input Actions asset

// Awake()
controls = new PlayerControls();
controls.Player1.MoveLeft.performed += PlayerMoveLeft;
controls.Player1.MoveLeft.canceled += PlayerMoveLeftReleased;
// move actions handled here ...

// create a method to handle the event for "MoveLeft" Action
void PlayerMoveLeft(InputAction.CallbackContext context)
{
	Debug.Log("Player will Move Left");
	// ...
}

// create a method to handle the event for "MoveLeft" Released Action
void PlayerMoveLeftReleased(InputAction.CallbackContext context)
{
	Debug.Log("Player will stop Moving Left");
	// ...
}


/* -----------------------------------------
   Direct Keyboard Control
----------------------------------------- */
// Update():

// first get the keyboard device
Keyboard keyboard = InputSystem.GetDevice<Keyboard>();

// check key pressed
if (keyboard.leftArrowKey.wasPressedThisFrame) {
	// LEFT ARROW key pressed ...
}

// check key released
if (keyboard.leftArrowKey.wasReleasedThisFrame) {
	// LEFT ARROW key released ...
}

// check key held
if (keyboard.leftArrowKey.isPressed) {
	// LEFT ARROW key held down ...
}

// OR use Keyboard.current
if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
	// LEFT ARROW key pressed ...
}


/* -----------------------------------------
   Direct Gamepad Control
----------------------------------------- */
// Update():

// first get the gamepad device
Gamepad gamepad = InputSystem.GetDevice<Gamepad>();

// check button pressed
if (gamepad.xButton.wasPressedThisFrame) {
	// X button pressed ...
}

// check button released
if (gamepad.xButton.wasReleasedThisFrame) {
	// X button released ...
}

// check button held
if (gamepad.xButton.isPressed) {
	// X button held down ...
}


/* -----------------------------------------
	Gamepad Vibration
----------------------------------------- */
// first get the gamepad device
Gamepad gamepad = InputSystem.GetDevice<Gamepad>();

// set the motor vibration speed (0 to 1), set to 0 to stop vibration
gamepad.SetMotorSpeeds(1, 1);


/* -----------------------------------------
   Handle Hardware Back Button (Android)
----------------------------------------- */
#if UNITY_ANDROID
// disable Hardware Back button quitting the app
Input.backButtonLeavesApp = false;

if (keyboard.escapeKey.wasPressedThisFrame) {
	// Back button pressed ... (maps to ESCAPE)
}
#endif


/* -----------------------------------------
   Mouse Position
----------------------------------------- */
// move GameObject at mouse x,y continuously

/// Update():
Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
this.gameObject.transform.position = mousePosition;

