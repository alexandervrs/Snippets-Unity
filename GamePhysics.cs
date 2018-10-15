
/**
 * GamePhysics.cs
 * General purpose game physics/calculation snippets for Unity
 */


/* -----------------------------------------
   Attract an Object
----------------------------------------- */
float attractionForce = 1.0f;
Vector2 direction = playerPosition - attractedObjectPosition;
direction.Normalize();
attractedObjectVelocity += direction * attractionForce;
attractedObjectPosition += attractedObjectVelocity;