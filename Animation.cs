
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

	note: If you want a custom default state, you can right click anywhere in the Animator window, create a New State
	      right click on the new state and choose "Set as Layer Default State"

	note: In an Animation, to add a default keyframe you first have to add the property you intent to animate
	
	note: To add a keyframe, you can right click on the property you need to animate and "Add Key" or hit the "K" key with 
	      the Animation window in focus. Or you can enable frame Recording and change the property you need to animate
	
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


/* -----------------------------------------
   Animate Sprites
----------------------------------------- */

/*

	To animate a series of sprites with Unity's Animation system there's a couple ways:

	a. Spritesheet
	
	1. Export your frames as a spritesheet from your animation program, make sure the frames are not trimmed
       & are aligned in a grid with each frame being the same size
	2. Import the spritesheet image and open Unity's Sprite Editor. At the top of that window choose to Slice
       the image either by columns/rows or frame width and height. Apply changes
	3. Create a new Animation and an Animator for the spritesheet
	4. Click to expand the spritesheet sub-sprites/frames, select them and drag and drop the frames into the Animation timeline
	5. While in the Animation window, click at the top right menu button and choose "Set Sample Rate" (or also "Show Sample Rate")
	   so you can see and change the framerate of the animation
	   
	   
	b. Individual Frames
	
	1. Export your frames as individual images
	2. Import the images in a folder inside your project
	3. Create a new Animation and an Animator for the frames
	4. Select and drag and drop the frames into the Animation timeline
	5. While in the Animation window, click at the top right menu button and choose "Set Sample Rate" (or also "Show Sample Rate")
	   so you can see and change the framerate of the animation
	6. You can also pack said images with a Sprite Atlas (Create > Sprite Atlas and drag and drop the image frames under "Objects for Packing")
	   for better performance
	   * Tip: You can drag & drop multiple textures by dragging them over the "Objects for Packing" title label
	
	note: Dragging and dropping a series of images in Unity "Scene" tab will prompt you to create a Unity Animation & Animator asset 
	      that can be used to animate these images
		  
*/
