
/**
 * Spine.cs
 * Spine related snippets for Unity
 *
 * http://esotericsoftware.com/spine-unity-download
 */

/* using */
using Spine; // for TrackEntry, AnimationState, Event, SkeletonData
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
SkeletonAnimation spineAnimation;

/// Start():
spineAnimation = gameObject.GetComponent<SkeletonAnimation>();

/// Start(), Update():
spineAnimation.AnimationName = "run"; // set to animation name
spineAnimation.timeScale     = 1.0f;  // animation speed timescale
spineAnimation.loop          = true;  // is looping
spineAnimation.initialFlipX  = false; // whether to flip X
spineAnimation.initialFlipY  = false; // whether to flip Y


/* -----------------------------------------
   Playback control
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineAnimation;

/// Start():
spineAnimation = gameObject.GetComponent<SkeletonAnimation>();

/* play animation */
spineAnimation.AnimationState.SetAnimation(0, "walk", false); // trackID, animationName, loop
spineAnimation.AnimationState.TimeScale = 1.0f;

// OR 
spineAnimation.AnimationName = "";     // clear animation & rewind
spineAnimation.AnimationName = "walk"; // set new animation
spineAnimation.timeScale     = 1.0f;   // play

/* stop animation */
string currentAnimation = spineAnimation.AnimationName;
spineAnimation.AnimationState.ClearTrack(0);
spineAnimation.AnimationState.SetAnimation(0, currentAnimation, false);
spineAnimation.AnimationState.TimeScale = 0.0f;
// OR 
string currentAnimation     = spineAnimation.AnimationName; // keep previous animation
spineAnimation.AnimationName = "";                          // clear animation & rewind
spineAnimation.AnimationName = currentAnimation;            // restore previous animation
spineAnimation.timeScale     = 0.0f;                        // stop

/* pause animation */
spineAnimation.AnimationState.TimeScale = 0.0f;
// OR 
spineAnimation.timeScale = 0.0f;

/* resume animation */
spineAnimation.AnimationState.TimeScale = 1.0f;
// OR 
spineAnimation.timeScale = 1.0f;


/* -----------------------------------------
   Animation info
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineAnimation;

/// Start():
spineAnimation = gameObject.GetComponent<SkeletonAnimation>();

// get animation duration of "walk" animation
float duration = spineAnimation.Skeleton.Data.FindAnimation("walk").duration;


/* -----------------------------------------
   Execute Spine Events
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineAnimation;

/// Start():
spineAnimation = gameObject.GetComponent<SkeletonAnimation>();

// first assign the event
spineAnimation.AnimationState.Event += OnSpineEvent; 

/// Class Body:

// next check for the event
void OnSpineEvent(TrackEntry trackEntry, Spine.Event e) 
{
    
    // check for Events labelled "sound" in Spine
    if (e.Data.Name == "sound") {
        Debug.Log("Sound event from Spine");
    }

}


/* -----------------------------------------
   Swap Skin
----------------------------------------- */
/// Class Body:
SkeletonAnimation spineAnimation;

/// Start():
spineAnimation = gameObject.GetComponent<SkeletonAnimation>();

spineAnimation.Skeleton.SetSkin("your_other_skin_name");


/* -----------------------------------------
   Fade Skin
----------------------------------------- */
/// Start(), Update():
gameObject.GetComponent<SkeletonRenderer>().skeleton.SetColor( new Color(1.0f, 1.0f, 1.0f, 0.5f) ); // r,g,b,a


/* -----------------------------------------
   List Skins
----------------------------------------- */
/// Start():
Skeleton     spineSkeleton     = gameObject.GetComponent<Skeleton>();
SkeletonData spineSkeletonData = spineSkeleton.Skeleton.Data;

foreach(Skin thisSkin in spineSkeletonData.Skins) {
    Debug.Log(thisSkin.Name);
}


/* -----------------------------------------
   Mix and Match Skin Items
----------------------------------------- */

/*

    a. Create a Spine project where the character is comprised by multiple Skins, e.g. character has a variety of faces and swords
       The hierarchy of a part should be like:

        sword : Bone
            sword: Slot
                sword: Skin Placeholder
                    * will be populated with different Image per Skin
        face : Bone
            face: Slot
                face: Skin Placeholder
                    * will be populated with different Image per Skin

    b. Skin and Images should be placed under their own folders, e.g. gold_sword and iron_sword would go under a "swords" folder.
       So the Skin hierarchy would be like:

       swords: Folder
           gold_sword: Skin
           iron_sword: Skin
       faces: Folder
           green_face: Skin
           blue_face: Skin

       Anything we need in every Skin should just be a regular Image outside of any Skin Placeholders, e.g. any body parts that never change

    c. To add an Image to a specific Skin, click on the Spine Editor hierarchy to make that particular Skin enabled & visible, then drag and drop the Image
       onto the Skin Placeholder that you want

    d. To swap out a Skin item in-game, we'd need to create a new Skin via a script and copy only the Skin items we need, then assign that
       new custom Skin to our Skeleton

*/

// get data about our Skeleton
Skeleton     spineSkeleton      = gameObject.GetComponent<Skeleton>();
SkeletonData spineSkeletonData  = spineSkeleton.Skeleton.Data;
Skin         currentSkin        = spineSkeleton.Skin;

// create a new custom skin
Skin customSkin = new Skin("custom");

// copy data from the current skin to the new custom skin
customSkin.AddSkin(currentSkin);
// change the sword to the gold_sword Skin item
customSkin.AddSkin(spineSkeletonData.FindSkin("swords/gold_sword"));

// set the Skin to be the custom skin we created
spineSkeleton.SetSkin(customSkin);
spineSkeleton.SetSlotsToSetupPose();

