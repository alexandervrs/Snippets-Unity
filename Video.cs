
/**
 * Video.cs
 * Video related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.Video;


/* -----------------------------------------
   Play Video Clip
----------------------------------------- */
// Start():

// load a video clip from the "Resources/Movie/" folder
VideoClip movieClip = Resources.Load<VideoClip>("Movie/Opening") as VideoClip;

// create a video player
VideoPlayer moviePlayer = GameObject.Find("Main Camera").AddComponent<VideoPlayer>();

// add an AudioSource for the video audio track
AudioSource movieAudio = GameObject.Find("Main Camera").AddComponent<AudioSource>();

// adjust video settings
moviePlayer.playOnAwake = false;                           // start immediately or wait
moviePlayer.renderMode  = VideoRenderMode.CameraNearPlane; // VideoRenderMode.CameraFarPlane will make the video show behind other GameObjects
moviePlayer.targetCameraAlpha = 1.0f;                      // will make the video texture transparent
moviePlayer.playbackSpeed = 1.0f;						   // set the playback speed
moviePlayer.isLooping = false;							   // set loop playback
moviePlayer.clip = movieClip;                              // the video clip to show
moviePlayer.SetTargetAudioSource(0, movieAudio);           // attach the AudioSource for sound to play

// play the video and invoke callback "OnVideoFinished" when video ends
moviePlayer.Play();                                       
moviePlayer.loopPointReached += OnVideoFinished;


// add an event for video ended callback
void OnVideoFinished(VideoPlayer vp)
{
	// stop the video
	vp.Stop(); 
	
	//detach the video player Component
	GameObject.Destroy(GameObject.Find("Main Camera").GetComponent<VideoPlayer>());

}

