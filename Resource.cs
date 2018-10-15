
/**
 * Resource.cs
 * Resource related snippets for Unity
 */

/* using */
using UnityEngine;


/*
	Note: Dynamically loaded assets should be placed inside a "Resources" folder
*/


/* -----------------------------------------
   Load Resource
----------------------------------------- */
// load SampleTexture.png from "Resources/Graphics/" folder
Texture2D tex = Resources.Load("Graphics/SampleTexture") as Texture2D;

// load BGM1.wav from the "Resources/Audio/" folder
AudioClip soundClip = Resources.Load<AudioClip>("Audio/BGM1") as AudioClip;

// load Opening.mp4 from the "Resources/Movie/" folder
VideoClip movieClip = Resources.Load<VideoClip>("Movie/Opening") as VideoClip;


/* -----------------------------------------
   Unload Resource
----------------------------------------- */
// unload SampleTexture asset
Resources.UnloadAsset(tex);


/* -----------------------------------------
   Unload Unused Assets
----------------------------------------- */
// start unloading
StartCoroutine(UnloadAssetsAsync());

// use ienumerator to unload and check status
IEnumerator UnloadAssetsAsync()
{
	
	AsyncOperation asyncUnload = Resources.UnloadUnusedAssets();
	
	if (asyncUnload == null) { 
		yield break;
	}
	
	while (!asyncUnload.isDone)
	{
		// query asyncUnload.progress here for progress (0.0f to 1.0f)
		yield return null;
	}
	
	// assets unloaded ...

}