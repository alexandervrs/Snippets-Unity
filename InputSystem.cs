
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
   Abstract Device Control with Actions
----------------------------------------- */

/*

	The new Input System abstracts device button presses, key presses, touches, clicks into Actions

	note: This is not 100% required, you can always use direct device control (see further below)
	      but Actions save time handling multiple types of input devices at the same time

	a. First go to Assets > Create > Input Actions
	b. Name the new asset to anything you like e.g. "PlayerControls" and click on it

	c. Create a new Action Map, named it anything you like e.g. "Player".
	   We can have different action maps with different bindings later, for e.g. Menu screens etc.

	d. Choose the new Action Map "Player" and click the + button to create a new Action
	e. Name it e.g. "Movement" and click the + button next to it to "Add 2D Vector Composite", give it a name
	   like "ArrowKeys", set the "Mode" to "Digital Normalized"

	f. Choose a key/button from the list, e.g. "Left Arrow [Keyboard]" and choose "Composite Part" to be 
	   the same direction, e.g. Left Arrow should be set to "Left". Add the rest of the directions

	g. Let's add one key/button but is going to be pressed once (not held down like direction)
	   Choose the new Action Map "Player" and click the + button to create a new Action, name it something like "Jump"
	h. Click the + button next to it to "Add Binding" & select a key/button from the list

	i. Add more Actions & Bindings as you see fit for the keyboard and different devices, Save and Close the window

	j. With the asset selected still, from the Inspector, check the option "Generate C# Class" and Apply
	k. Now you're ready to access those Actions from your own Scripts

*/

// Class Body:
public PlayerControls controls; // "PlayerControls" is the name that you gave the Input Actions asset

private InputAction inputActionMovement; // We need "Movement" Action accessible from Update() later on

private Vector2 directionVector; // Vector 2D that results from the "Movement" Action

// (Only if you use Physics)
private Rigidbody2D physicsBody; // A Rigidbody2D or Ridigbody (3D) if you want to move the gameObject and interact with Physics

private float speed = 3.5f; // simple movement speed for the player, you might need to icrease this if you're using Physics

// Awake():
// initialize the controls
controls = new PlayerControls();

// bind the "Jump" action to a callback since it's going to be executed once per press
controls.Player.Jump.performed += PlayerJump(); // "performed" checks if that button was pressed, "canceled" will check if that button was released
// move actions handled here if needed ...

// get the "Movement" action reference for later
inputActionMovement = controls.Player.Movement;

// (Only if you use Physics) get the gameObject's Rigidbody component
physicsBody = gameObject.GetComponent<Rigidbody2D>(); // or "Rigidbody" if you use 3D Physics


// create a method to handle the event for "Jump" Action
public void PlayerJump()
{
	Debug.Log("Jump button pressed");
	// do any jump-related operations here, you can store a "hasJumped" value in a boolean
	// to use it later in your Update() or FixedUpdate() for example
}


// very important to enable the controls OnEnable(), otherwise nothing will work
// OnEnable(), OnDisable():
void OnEnable()
{
    controls.Enable();
}

void OnDisable()
{
    controls.Disable();
}

// Update():
// check for directional input
directionVector = inputActionMovement.ReadValue<Vector2>();

// MOVE THE TRANSFORM

// move the gameObject's Transform, will not interact with Physics
// Update(): 
if (directionVector != Vector2.zero) {

	// 2D movement (send input X to player's X and input Y to player's Y, do not change player's Z)
	gameObject.transform.position += (new Vector3(directionVector.x, directionVector.y, 0) * speed) * Time.deltaTime;

	// 3D movement (send input X to player's X and input Y to player's Z, do not change player's Y)
	//gameObject.transform.position += (new Vector3(directionVector.x, 0, directionVector.y) * speed) * Time.deltaTime;

}

// OR MOVE THE RIGIDBODY

// (Only if you use Physics) move the gameObject's Rigidbody, will also interact with Physics
// FixedUpdate():
if (directionVector != Vector2.zero) {

	// 2D movement (send input X to player's X and input Y to player's Y, do not change player's Z)
	physicsBody.MovePosition((new Vector3(directionVector.x, directionVector.y, 0) * speed) * Time.deltaTime);

	// 3D movement (send input X to player's X and input Y to player's Z, do not change player's Y)
	//physicsBody.MovePosition((new Vector3(directionVector.x, 0, directionVector.y) * speed) * Time.deltaTime);

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

