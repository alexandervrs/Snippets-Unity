
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
// load SampleTexture.png from "Resources/Graphics/" folder as Texture2D
Texture2D tex = Resources.Load<Texture2D>("Graphics/SampleTexture");

// load BGM1.wav from the "Resources/Audio/" folder as AudioClip
AudioClip soundClip = Resources.Load<AudioClip>("Audio/BGM1");

// load Opening.mp4 from the "Resources/Movie/" folder as VideoClip
VideoClip movieClip = Resources.Load<VideoClip>("Movie/Opening");

// load Descriptions.json / .txt from the "Resources/Content/" folder as TextAsset
TextAsset textAsset = Resources.Load<TextAsset>("Content/Descriptions");

// load Verdana Font from the "Resources/Fonts/" folder as TextAsset
Font fontAsset = Resources.Load<Font>("Fonts/Verdana");


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