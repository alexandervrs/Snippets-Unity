
/**
 * Camera.cs
 * Camera related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Find Main Camera
----------------------------------------- */
Camera mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();


/* -----------------------------------------
   Camera Look Around Object in 3D
----------------------------------------- */
// Class Body:
public Transform target;            // the target object
public float     speedMod = 10.0f;  // a speed modifier
private Vector3  point;             // the coord to the point where the camera looks at
   
// Start():
point = target.transform.position;  // get target's coords
gameObject.transform.LookAt(point); // makes the camera look to it

// Update():
// makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
gameObject.transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);


/* -----------------------------------------
   Sprite Always Looks at Camera
----------------------------------------- */
// Update():
// make GameObject's sprite always face the camera
gameObject.transform.LookAt(GameObject.Find("Main Camera").transform.position, Vector3.up);

