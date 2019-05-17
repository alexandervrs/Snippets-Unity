
/**
 * Input.cs
 * Input related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Keyboard Control
----------------------------------------- */
// Update():

// check key pressed
if (Input.GetKeyDown(KeyCode.Escape)) {
	// ESCAPE key pressed ...
}

// check key released
if (Input.GetKeyUp(KeyCode.Escape)) {
	// ESCAPE key released ...
}

// check key held
if (Input.GetKey(KeyCode.Escape)) {
	// ESCAPE key held down ...
}


/* -----------------------------------------
   Mouse Control
----------------------------------------- */
// check mouse click on object (Requires a BoxCollider2D component)
void OnMouseDown() {

	if (Input.GetMouseButtonDown(0)) {
		
		var ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100)) {

			if (hit.collider.gameObject) {
				// clicked on object ...
			}

		}
		
	}

}


// move GameObject at mouse x,y continuously
// Update():
Vector3 mousePosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, gameObject.transform.position.z);


// move GameObject by drag and drop
// Class Body:
Vector3 screenSpace;
Vector3 offset;

void OnMouseDown(){
	
	Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    screenSpace = mainCamera.WorldToScreenPoint(transform.position);
    offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenSpace.z));
   
}

void OnMouseDrag () {
 
	Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);    
    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    transform.position = curPosition;

}

// hide the mouse cursor
Cursor.visible = false;

// lock the mouse cursor to the center of the view
Cursor.lockState = CursorLockMode.Locked;

// release the mouse cursor
Cursor.lockState = CursorLockMode.None;


/* -----------------------------------------
   Gamepad Control
----------------------------------------- */
// Update():

// check button pressed
if (Input.GetKeyDown(KeyCode.JoystickButton1)) {
	// Button key pressed ...
}

// check button released
if (Input.GetKeyUp(KeyCode.JoystickButton1)) {
	// Button key released ...
}

// check button held
if (Input.GetKey(KeyCode.JoystickButton1)) {
	// Button key held down ...
}

// check axis held raw (instantaneous)
if (Input.GetAxisRaw("Horizontal") < 0) {
	// Axis moved left ...
	
	// note: Axis names are set in the Input Manager
	//       Edit > Project Settings > Input
	// 
	//       "Horizontal": -1 = left, 1 = right
	//       "Vertical":   -1 = down, 1 = up
	//                      0 = none
}


/* -----------------------------------------
   Accelerometer Control (Tilt)
----------------------------------------- */
// Update():

// add a speed modifier
float speed = 32f;

// get 2D acceleration
Vector3 direction = new Vector3 (Input.acceleration.x, Input.acceleration.y, 0.0f);

// deccelerate
float friction = 0.05f;
direction.x = Mathf.Max(0, Mathf.Abs(Input.acceleration.x)-friction)*Mathf.Sign(Input.acceleration.x);
direction.y = Mathf.Max(0, Mathf.Abs(Input.acceleration.y)-friction)*Mathf.Sign(Input.acceleration.y);

// normalize vector
if (direction.sqrMagnitude > 1) {
	direction.Normalize();
}

// move
transform.Translate((direction*Time.deltaTime) * speed);

Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

// account for screen bounds and re-adjust position
Vector3 minScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
Vector3 maxScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth, mainCamera.pixelHeight, 0));

float edgeOffsetX = 0.2f;
float edgeOffsetY = 0.2f;
Vector3 clampedPos = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + edgeOffsetX, maxScreenBounds.x - edgeOffsetX),Mathf.Clamp(transform.position.y, minScreenBounds.y + edgeOffsetY, maxScreenBounds.y - edgeOffsetY), transform.position.z);
transform.position = clampedPos;


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


/* -----------------------------------------
   Handle Hardware Back Button (Android)
----------------------------------------- */
#if UNITY_ANDROID
// disable Hardware Back button quitting the app
Input.backButtonLeavesApp = false;

if (Input.GetKeyDown(KeyCode.Escape)) {
	// Back button pressed ... (maps to ESCAPE)
}
#endif



