
/**
 * Physics.cs
 * Physics related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Setup
----------------------------------------- */
/*

    1. Setup Physics via Edit > Project Settings > Physics/Physics 2D, especially tweak Y Gravity (e.g. -50), once
	2. If you need the Objects to slide against each other, you can assign a Default Physics Material with Dynamic and Static friction set to 0
	3. Add the GameObject with a Collider Component (e.g. Box Collider) & a Rigidbody (note: 2D elements need Collider2D/Rigidbody2D)
    4. Set Rigidbody to:
       Mass: 100, Drag: 1, Angular Drag: 0.9, Use Gravity (disabled if needed), Interpolate, Detection: Continuous Dynamic.
       Freeze axes that you don't want to be affected automatically by the Physics.
    5. Add another GameObject to serve as obstacle, add also a Collider or Collider2D to it

    note: All Physics/Rigidbody etc. update checks must be done in FixedUpdate() instead of Update()
          FixedUpdate() runs at a different rate than regular Update().
          Do not animate the gameObject's Transform if you want its Rigidbody to also interact with Physics, use MovePosition() or AddForce() instead.
          That said Input checks should be done in Update() to avoid input lag. You can check the input in Update() and pass that result in FixedUpdate()
	
*/


/* -----------------------------------------
   Apply Forces
----------------------------------------- */
/// Class Body:
Rigidbody physicsBody;

/// Start():
physicsBody = gameObject.GetComponent<Rigidbody>();

/// Start(), FixedUpdate():

// changes the position of an object
physicsBody.MovePosition(new Vector3(1, 1, 0));

// add force, affects position (x,y,z)
physicsBody.AddForce(4000, 4000, 4000);

// add force relative to the object's coordinate system, affects position (x,y,z)
physicsBody.AddRelativeForce(4000, 4000, 4000);

// changes the rotation of an object
physicsBody.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 60)));

// add torque, affects rotation (x,y,z)
physicsBody.AddTorque(4000, 4000, 4000);

// add torque relative to the object's coordinate system, affects rotation (x,y,z)
physicsBody.AddRelativeTorque(4000, 4000, 4000);

// add force with torque, affects position & rotation (x,y,z)
physicsBody.AddForceAtPosition(new Vector3(12000, 12000, 12000), transform.position, ForceMode.Force);


/* -----------------------------------------
   Control Properties
----------------------------------------- */
/// Class Body:
Rigidbody physicsBody;

/// Start():
physicsBody = gameObject.GetComponent<Rigidbody>();

/// Start(), FixedUpdate():

// allows for infinite rotation/torque
physicsBody.maxAngularVelocity = Mathf.Infinity;

// disables physics manipulating the rotation of an object
physicsBody.freezeRotation = true;

// controls whether gravity affects the object
physicsBody.useGravity = false;

// returns whether the body has stopped being affected by physics forces
bool isAsleep = physicsBody.IsSleeping();

// makes an object asleep for at least one frame
physicsBody.Sleep();

// wakes up an object
physicsBody.WakeUp();

// set the mass of an object
physicsBody.SetDensity(6.0f);


/* -----------------------------------------
   Collision Events
----------------------------------------- */

/* 
   note: Collision events need a Collider and Rigidbody Component in order to fire
         OnTrigger* events are executed if the Collider is marked as a Trigger and 
         does not apply any forces when the GameObject collides with another
  
         3D collision uses Collision & OnCollision* / OnTrigger* events
         2D collision uses Collision2D & OnCollision*2D / OnTrigger*2D events
*/
		 
/// Class Body:

void OnCollisionEnter(Collision collisionInfo)
{
    Debug.Log("On Collision Enter with "+collisionInfo.gameObject.name); 
}

void OnCollisionStay(Collision collisionInfo)
{
    Debug.Log("On Collision Stay with "+collisionInfo.gameObject.name);
}

void OnCollisionExit(Collision collisionInfo)
{
    Debug.Log("On Collision Exit with "+collisionInfo.gameObject.name);
}

void OnTriggerEnter(Collider collisionInfo)
{
    Debug.Log("On Trigger Enter with "+collisionInfo.gameObject.name); 
}

void OnTriggerStay(Collider collisionInfo)
{
    Debug.Log("On Trigger Stay with "+collisionInfo.gameObject.name);
}

void OnTriggerExit(Collider collisionInfo)
{
    Debug.Log("On Trigger Exit with "+collisionInfo.gameObject.name);
}


/* -----------------------------------------
   Raycast Collision 2D
----------------------------------------- */
/// FixedUpdate():

RaycastHit2D hit = Physics2D.Raycast(
    gameObject.transform.position, 
    new Vector3(gameObject.transform.position.x+0.5f, // check for Collisions +0.5f to the right side of the collider
                gameObject.transform.position.y, 
                gameObject.transform.position.z), 
    10, // how much to extend the check
    LayerMask.GetMask("Default") // which Layer to check Collisions for, usually you want a layer for "Solid" objects, that way the Raycast won't check itself
);

// if Raycast hit a Collider
if (hit.collider != null)
{
    // if Collider has a certain tag (e.g. Solid)
    if (hit.collider.tag == "Solid") {

        Debug.LogError(gameObject.name+" collides with "+hit.collider.name);

    }
}


/* -----------------------------------------
   Raycast Collision 3D
----------------------------------------- */
/// FixedUpdate():

RaycastHit hit;
Physics.Raycast(
    gameObject.transform.position, 
    new Vector3(gameObject.transform.position.x+0.5f, // check for Collisions +0.5f to the right side of the collider
                gameObject.transform.position.y, 
                gameObject.transform.position.z), 
                out hit,
    10, // how much to extend the check
    LayerMask.GetMask("Default")  // which Layer to check Collisions for, usually you want a layer for "Solid" objects, that way the Raycast won't check itself
);

// if Raycast hit a Collider
if (hit.collider != null)
{
    // if Collider has a certain tag (e.g. Solid)
    if (hit.collider.tag == "Solid") {

        Debug.LogError(gameObject.name+" collides with "+hit.collider.name);

    }
}
