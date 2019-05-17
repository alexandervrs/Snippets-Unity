
/**
 * Spine.cs
 * Spine related snippets for Unity
 *
 * http://esotericsoftware.com/spine-unity-download
 */

/* using */
using Spine; // for TrackEntry, AnimationState, Event
using Spine.Unity; // for SkeletonAnimation, SkeletonRenderer


/* -----------------------------------------
   Setup Spine
----------------------------------------- */

/*
	1. In your Unity project, you need the runtime installed once, get the package from http://esotericsoftware.com/spine-unity-download
    2. Export your animations from Spine, select JSON
    3. Create Atlas & click on Settings. Unity needs the Spine Atlas file exported from Spine with .atlas.txt extension,
       change it under Options > Atlas Extension
    4. Drag and drop the exported assets in Unity (atlas.txt, images, json) to import the skeleton data and images
    5. Drag and drop the SkeletonData asset in your Scene and create Spine Animation GameObject from it
*/


/* -----------------------------------------
   Change Skeleton Animation
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineSkeleton;

/// Start():
spineSkeleton = gameObject.GetComponent<SkeletonAnimation>();

/// Start(), Update():
spineSkeleton.AnimationName = "run"; // set to animation name
spineSkeleton.timeScale     = 1.0f;  // animation speed timescale
spineSkeleton.loop          = true;  // is looping
spineSkeleton.initialFlipX  = false; // whether to flip X
spineSkeleton.initialFlipY  = false; // whether to flip Y


/* -----------------------------------------
   Playback control
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineSkeleton;

/// Start():
spineSkeleton = gameObject.GetComponent<SkeletonAnimation>();

/* play animation */
spineSkeleton.AnimationState.SetAnimation(0, "walk", false); // trackID, animationName, loop
spineSkeleton.AnimationState.TimeScale = 1.0f;

// OR 
spineSkeleton.AnimationName = "";     // clear animation & rewind
spineSkeleton.AnimationName = "walk"; // set new animation
spineSkeleton.timeScale     = 1.0f;   // play

/* stop animation */
string currentAnimation = spineSkeleton.AnimationName;
spineSkeleton.AnimationState.ClearTrack(0);
spineSkeleton.AnimationState.SetAnimation(0, currentAnimation, false);
spineSkeleton.AnimationState.TimeScale = 0.0f;
// OR 
string currentAnimation     = spineSkeleton.AnimationName; // keep previous animation
spineSkeleton.AnimationName = "";                          // clear animation & rewind
spineSkeleton.AnimationName = currentAnimation;            // restore previous animation
spineSkeleton.timeScale     = 0.0f;                        // stop

/* pause animation */
spineSkeleton.AnimationState.TimeScale = 0.0f;
// OR 
spineSkeleton.timeScale = 0.0f;

/* resume animation */
spineSkeleton.AnimationState.TimeScale = 1.0f;
// OR 
spineSkeleton.timeScale = 1.0f;


/* -----------------------------------------
   Animation info
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineSkeleton;

/// Start():
spineSkeleton = gameObject.GetComponent<SkeletonAnimation>();

// get animation duration
float duration = spineSkeleton.Skeleton.Data.FindAnimation("walk").duration;


/* -----------------------------------------
   Execute Spine Events
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineSkeleton;

/// Start():
spineSkeleton = gameObject.GetComponent<SkeletonAnimation>();

// first assign the event
spineSkeleton.AnimationState.Event += OnSpineEvent; 

/// Class Body:

// next check for the event
void OnSpineEvent(TrackEntry trackEntry, Spine.Event e) {
    
    // check for Events labelled "sound" in Spine
    if (e.Data.Name == "sound") {
        Debug.Log("Sound event from Spine");
    }

}


/* -----------------------------------------
   Swap Skin
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineSkeleton;

/// Start():
spineSkeleton = gameObject.GetComponent<SkeletonAnimation>();

spineSkeleton.Skeleton.SetSkin("newSkin");


/* -----------------------------------------
   Fade Skin
----------------------------------------- */
/// Start(), Update():
gameObject.GetComponent<SkeletonRenderer>().skeleton.SetColor( new Color(1.0f, 1.0f, 1.0f, 0.5f) ); // r,g,b,a

