
/**
 * NavMesh.cs
 * Navigation Mesh related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.AI; // for NavMeshAgent

/*

	Unity's NavMesh currently only works with 3D 
	For 2D support either use a A* Pathfinding solution or
	a 2D variant like NavMeshPlus (https://github.com/h8man/NavMeshPlus)

*/

/* -----------------------------------------
   Setup NavMesh
----------------------------------------- */

/*

	First we need to define the area of allowed movement/pathfinding & which GameObjects will act as obstacles:

	1. From the main menu, go to Window > AI > Navigation, to show the Navigation panel
	2. Select the GameObjects in the Scene that will create a "walkable floor"
	3. With these GameObjects selected, in the Navigation panel, select the "Object" tab, check "Navigation Static"(*) option
	   and set the "Navigation Area" to "Walkable"
	4. Select the GameObjects in the Scene that will create a "non-walkable wall" or otherwise obstacle
	5. With these GameObjects selected, in the Navigation panel, select the "Object" tab, check "Navigation Static"(*) option
	   and set the "Navigation Area" to "Non Walkable"
	6. In the Navigation panel, select the "Bake" tab, change any parameters as needed and
	   click the "Bake" button.
	
	(*) note: If the GameObject will be moving around the Scene, then it will need to be calculated on runtime, as such it cannot
	          use "Navigation Static". In case the obstacle GameObject will be moving around it must also have a "Nav Mesh Obstacle" Component
			  (Inspector window > Add Component > Nav Mesh Obstacle) attached to it.
	
*/


/* -----------------------------------------
   Setup & Move NavMesh Agent
----------------------------------------- */

/*

	Secondly we need to setup NavMeshAgent components for all the AI controlled GameObjects:
	
	1. Select the GameObject and from the Inspector window > Add Component > Nav Mesh Agent
	   Tweak settings as needed. Usually we might need to change Speed & Obstacle Avoidance Radius
	2. To actually move the GameObject, we will need a new C# Script with the and attach it to the GameObject
	   Script contents will be as follows:

*/	

/// Class Body:
public  GameObject destination; // this is the GameObject we want to reach/pathfind to
private NavMeshAgent nMeshAgent;
	
/// Start():
nMeshAgent = gameObject.GetComponent<NavMeshAgent>();
	
/// Start(), Update():
// move to the GameObject's position specified in "destination" variable avoiding regions outside the NavMesh bounds
// and GameObjects with Nav Mesh Obstacle Components. Using this in a custom method/call or in Start() will cause
// the NavMesh Agent GameObject to calculate a path and move to the destination once. Using this in Update() will
// cause the NavMesh Agent GameObject to track the position and move towards the destination observing any changes
nMeshAgent.SetDestination(destination.transform.position);

// OR

// instantly jumps the NavMesh Agent to its destination, will perform any pathfinding as normal
// this method will return false if the NavMesh Agent could not reach the destination (obstacle in the way etc.)
nMeshAgent.Warp(destination.transform.position);


/* -----------------------------------------
   NavMeshAgent Fine Control
----------------------------------------- */

// should the NavMeshAgent update the GameObject's rotation? (default: true)
nMeshAgent.updateRotation = true;

// should the NavMeshAgent update the GameObject's position? (default: true)
// use this to pause the position update in order to jump the GameObject somewhere later
nMeshAgent.updatePosition = true;

// get the NavMeshAgent's velocity
float navMeshAgentVelocity = nMeshAgent.velocity;

// manually set the NavMeshAgent's velocity
nMeshAgent.velocity = 2;

// should the NavMeshAgent start slowing down as it reaches the destination?
nMeshAgent.autoBraking = true;

// returns if the NavMeshAgent is stopped
bool isStopped = nMeshAgent.isStopped;

// stops the NavMeshAgent's movement
nMeshAgent.Stop();
