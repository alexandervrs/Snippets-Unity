
/**
 * Spine.cs
 * Spine related snippets for Unity
 *
 * http://esotericsoftware.com/spine-unity-download
 */

/* using */
using Spine.Unity;


/* -----------------------------------------
   Setup New Spine SkeletonData
----------------------------------------- */
/*
	Unity needs the Spine Atlas file exported from Spine with .atlas.txt extension
*/


/* -----------------------------------------
   Change Skeleton Animation
----------------------------------------- */

SkeletonAnimation spineAnim;

spineAnim = GetComponent<SkeletonAnimation>();

spineAnim.AnimationName = "run"; // set to animation name
spineAnim.timeScale     = 1.0f;  // animation speed timescale
spineAnim.loop          = true;  // is looping
spineAnim.initialFlipX  = false; // whether to flip X
spineAnim.initialFlipY  = false; // whether to flip Y

