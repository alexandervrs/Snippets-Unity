
/**
 * Camera.cs
 * Camera related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Setup Camera
----------------------------------------- */

/* 

    1. Select "Game" tab, add a couple more Resolutions in the dropdown at the top left
       Add 16:9 Landscape & 22:10 Landscape
    
    2. Click on your Camera (usually named "Main Camera")
       Change Orthographic Size, 5.4 is proper for full view in the above aspect ratios
	
 */


/* -----------------------------------------
   Find Main Camera
----------------------------------------- */
Camera mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();


/* -----------------------------------------
   Get Camera Info
----------------------------------------- */
/// Class Body:
Camera mainCam;

/// Start(), Awake():
mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

/// Start(), Awake(), Update():

// get size in pixels
int pixelWidth  = mainCam.pixelWidth;
int pixelHeight = mainCam.pixelHeight;

// get size in pixels (account for scaling)
int pixelWidth  = mainCam.scaledPixelWidth;
int pixelHeight = mainCam.scaledPixelHeight;

// get size in units
float unitHeight = mainCam.orthographicSize;
float unitWidth  = unitHeight * mainCam.aspect;

// get aspect ratio (ratio = (width / height))
float aspectRatio = mainCam.aspect;


/* -----------------------------------------
   Zoom Camera
----------------------------------------- */
/// Class Body:
Camera mainCam;
float baseZoom;
float zoom;

/// Start(), Awake():
mainCam  = GameObject.Find("Main Camera").GetComponent<Camera>();
baseZoom = mainCam.orthographicSize; // base zoom, usually 5.4

/// Start(), Awake(), Update():
zoom = 1.0f; // change this to zoom

// setting orthographicSize makes it an easy way to zoom in and out
mainCam.orthographicSize = baseZoom * zoom;


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


/* -----------------------------------------
   Sprite Fill Camera View
----------------------------------------- */
/// Class Body:
Camera mainCam;
SpriteRenderer spriteRenderer;

/// Start(), Awake():
mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

/// Start(), Update():
float cameraHeight = mainCam.orthographicSize * 2;
Vector2 cameraSize = new Vector2(mainCam.aspect * cameraHeight, cameraHeight);
Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

Vector2 scale = transform.localScale;

if (cameraSize.x >= cameraSize.y) {
    // landscape
    scale *= cameraSize.x / spriteSize.x;
} else {
    // portrait
    scale *= cameraSize.y / spriteSize.y;
}

gameObject.transform.position   = Vector2.zero; // move to world 0,0 (optional)
gameObject.transform.localScale = scale; // apply scale

