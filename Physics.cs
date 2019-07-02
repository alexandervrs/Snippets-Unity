
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
       Mass: 100, Drag: 1, Angular Drag: 0.9, Use Gravity (disabled if needed), Interpolate, Detection: Continuous Dynamic
    5. Add another GameObject to serve as obstacle, add also a Collider or Collider2D to it
	
*/


/* -----------------------------------------
   Apply Forces
----------------------------------------- */
/// Class Body:
Rigidbody physicsBody;

/// Start():
physicsBody = gameObject.GetComponent<Rigidbody>();

/// Start(), Update():

// add force, affects motion (x,y,z)
physicsBody.AddForce(4000, 4000, 4000);

// add torque, affects rotation (x,y,z)
physicsBody.AddTorque(4000, 4000, 4000);

// add force relative to the object's coordinate system, affects motion (x,y,z)
physicsBody.AddRelativeForce(4000, 4000, 4000);

// add torque relative to the object's coordinate system, affects rotation (x,y,z)
physicsBody.AddRelativeTorque(4000, 4000, 4000);

// add force with torque, affects motion & rotation (x,y,z)
physicsBody.AddForceAtPosition(new Vector3(12000, 12000, 12000), transform.position, ForceMode.Force);


/* -----------------------------------------
   Control Properties
----------------------------------------- */
/// Class Body:
Rigidbody physicsBody;

/// Start():
physicsBody = gameObject.GetComponent<Rigidbody>();

/// Start(), Update():

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

// sets the rotation of an object
physicsBody.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 0)));

// sets the position of an object
physicsBody.MovePosition(new Vector3(0, 0, 0));


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

void OnTriggerEnter(Collision collisionInfo)
{
    Debug.Log("On Trigger Enter with "+collisionInfo.gameObject.name); 
}

void OnTriggerStay(Collision collisionInfo)
{
    Debug.Log("On Trigger Stay with "+collisionInfo.gameObject.name);
}

void OnTriggerExit(Collision collisionInfo)
{
    Debug.Log("On Trigger Exit with "+collisionInfo.gameObject.name);
}

