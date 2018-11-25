
/**
 * Transform.cs
 * Transform related snippets for Unity
 */

/* using */
using UnityEngine;


/* 
	Important Note:
		
	Using "material" to change properties like color/alpha will cause Unity to create a copy of that material 
	so you can commit your changes without affecting other objects using the same material

*/

Material mMaterial;

void Start() 
{
	// get and store the material index
	mMaterial = GetComponent<Renderer>().material;
}

void OnDestroy()
{
	// destroy the material on object's Destroy event
	Destroy(mMaterial);
}
		
/*		
	You'd also need to use Resources.UnloadUnusedAssets() when done with the scene to remove those copies from memory if they exist

	Alternatively you could use sharedMaterial which does not need Resources.UnloadUnusedAssets() 
	but will also affect other objects' materials
*/


/* -----------------------------------------
   Set transform values
----------------------------------------- */
// set position
gameObject.transform.position = new Vector3(
	gameObject.transform.position.x, 
	gameObject.transform.position.y-1.0f, 
	gameObject.transform.position.z
);

// set rotation (or localRotation for local axis rotation)
gameObject.transform.rotation = Quaternion.Euler(
	gameObject.transform.rotation.x-90.0f, 
	gameObject.transform.rotation.y, 
	gameObject.transform.rotation.z
);

// set scale
gameObject.transform.localScale = new Vector3(
	gameObject.transform.localScale.x-0.5f, 
	gameObject.transform.localScale.y-0.5f, 
	gameObject.transform.localScale.z-0.5f
);

// set alpha
gameObject.transform.GetComponent<Renderer>().material.color = new Vector4(1.0f, 1.0f, 1.0f, 0.4f); // r,g,b,a


/* -----------------------------------------
   Align to another Object
----------------------------------------- */
// center current gameobject to main camera
gameObject.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );


/* -----------------------------------------
   Rotate Object locally around its pivot
----------------------------------------- */
/// Class Body:
Vector3 rotateBy;

/// Start():
rotateBy = new Vector3(0, 4, 0); // rotate by X axis (in 2D view)

/// Update():
transform.localRotation *= Quaternion.Euler(rotateBy.x, rotateBy.y, rotateBy.z);


/* -----------------------------------------
   Tween to another Object position
----------------------------------------- */
public  AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f); // curve to use for smoothing
private Transform start; // start position (GameObject's current position)
public  Transform end;  // end position (another GameObject's transform)
public  float duration = 1.0f; // duration of the tween
private float t; // time tick

// for checking if ended
private float timeStartedLerping;
private bool  isLerping;

/// Start():
this.start = this.gameObject.transform; // get current position
t = 0.0f;

// start tween on awake
isLerping = true;
timeStartedLerping = Time.time;

/// Update():
if (this.end == null) { return; }

if (isLerping) {

	float timeSinceStarted   = Time.time - timeStartedLerping;
	float percentageComplete = timeSinceStarted / this.duration;

	t += Time.deltaTime;
	float s = t / this.duration;

	if (percentageComplete < 1.0f) {

		transform.position = Vector3.LerpUnclamped(this.start.position, this.end.position, this.curve.Evaluate(s)); // curve can also be an easing function instead

	} else {

		// tween complete
		isLerping = false;

	}

}


/* -----------------------------------------
   Sinewave Motion for Object
----------------------------------------- */
/// Class Body:
public float distanceX = 2.0f;
public float distanceY = 2.0f;

public float speedX = 0.0f;
public float speedY = 2.0f;

private float index = 0.0f;

private Vector3 initialPos;

/// Start():
initialPos = gameObject.transform.position; // keep initial position

/// Update():
index += Time.deltaTime;

float newPosX = (distanceX * Mathf.Sin(speedX * index));
float newPosY = (distanceY * Mathf.Sin(speedY * index));

gameObject.transform.position = new Vector3(initialPos.x + newPosX, initialPos.y + newPosY, gameObject.transform.position.z);

