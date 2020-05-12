
/**
 * Animation.cs
 * Animator & Animation related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Setup State Machine
----------------------------------------- */

/*

	note: On names, "Animator" concerns the "State Machine", manages when the Animations will play
		  "Animation" concerns the actual Animation of an object, what properties animate and how but not when it will play.
		  You can animate the properties of multiple GameObjects in a single Animation

	To Setup a StateMachine (Animator) and a couple Animations (Animation):
		  
	1.  Assets > Create > Animation Controller, give it a name like "PlayerStates"
	2.  Assets > Create > Animation, name it "PlayerIdle". Create a second one and name it "PlayerMove"
	3.  Window > Animation > Animator
	4.  Right click in Animator window > Create State > Empty. Do it twice for the animations that you have
	5.  Name the states "idle" and "move", click on their fields called "Motion" to assign an Animation
	6.  Right click "Entry" and "Make Transition" towards the idle state if you wish the idle to be the default animation
	7.  Go to your Scene, click on your GameObject and "Add Component", Add an "Animator" component
	8.  Under the Animator component, change "Controller" and set it to the "PlayerStates" state machine you have created
	9.  With the GameObject still selected, choose Window > Animation > Animation
	10. To the top left of the panel there should be a dropdown with all the assigned animations in the state machine
	11. Choose an animation and then click on "Add Property", animate/change the property you wish
	
	note: Dragging and dropping a series of images in Unity "Scene" tab will create a Sprite animation that you can use for these
		  states and change their frame timing as you wish from the Animation window

	note: If you want a custom default state, you can right click anywhere in the Animator window, create a New State
	      right click on the new state and choose "Set as Layer Default State"

	note: In an Animation, to add a default keyframe, you have to right click on the Property you want to animate and "Add Key" 
	      or hit the "K" key. Remember to do this before animating anything
	
*/

/* -----------------------------------------
   Change Animation State
----------------------------------------- */
/// Class Body:
Animator stateAnimator;

int idleState;
int moveState;
// more states here ...

/// Awake(), Start():
stateAnimator = GetComponent<Animator>(); // get the Animator component that you have attached

// get the hash IDs for the animation states so you can reference them later in a reliable manner
idleState = Animator.StringToHash("idle");
moveState = Animator.StringToHash("move");
// more states here ...

/// LateUpdate():

// get info for the current state of your object
AnimatorStateInfo animStateInfo = stateAnimator.GetCurrentAnimatorStateInfo(0);

// test changing state from "idle" -> "move" by pressing the Space key
if (Input.GetKeyDown(KeyCode.Space) && animStateInfo.shortNameHash == idleState) {
	// change state and play the Animation associated with it
	stateAnimator.Play(moveState);
}

