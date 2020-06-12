
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
moviePlayer.waitForFirstFrame = true;                      // whether the player will wait for first frame before playback, used when playOnAwake is on
moviePlayer.renderMode  = VideoRenderMode.CameraNearPlane; // VideoRenderMode.CameraFarPlane will make the video show behind other GameObjects
moviePlayer.skipOnDrop  = true;                            // if player is allowed to frameskip in order to be synced better with the game
moviePlayer.targetCameraAlpha = 1.0f;                      // will make the video texture transparent
moviePlayer.playbackSpeed = 1.0f;						   // set the playback speed
moviePlayer.isLooping = false;							   // set loop playback
moviePlayer.clip = movieClip;                              // the video clip to show
moviePlayer.SetTargetAudioSource(0, movieAudio);           // attach the AudioSource for sound to play

// ready the video player, preload the video frames and invoke callback "OnVideoPrepared" when video is ready
moviePlayer.Prepare();
moviePlayer.prepareCompleted += OnVideoPrepared;

// play the video and invoke callback "OnVideoFinished" when video ends. Might want to play the video when player is prepared
moviePlayer.Play();                                       
moviePlayer.loopPointReached += OnVideoFinished;

// invoke "OnVideoError" when an loading error occurs, e.g. HTTP error if loading the video from a remote source
moviePlayer.errorReceived += OnVideoError;

// add an event for video prepared callback
void OnVideoPrepared(VideoPlayer vp)
{
	// maybe play the video here?
	vp.Play();
}

// add an event for video ended callback
void OnVideoFinished(VideoPlayer vp)
{
	// stop the video
	vp.Stop(); 
	
	//detach the video player Component
	GameObject.Destroy(GameObject.Find("Main Camera").GetComponent<VideoPlayer>());

}

// add an event for video error callback
void OnVideoError(VideoPlayer vp, string message)
{

	Debug.LogError($"Error while loading the video: {message}");

	vp.errorReceived -= OnVideoError; // unregister to avoid memory leaks

}


/* -----------------------------------------
   Get Video Info
----------------------------------------- */
// is video playing
bool isVideoPlaying = moviePlayer.isPlaying;

// is video paused
bool isVideoPlaying = moviePlayer.isPaused;

// is video prepared? Will still report false if there's a loading error, check "errorReceived" for any errors
bool isVideoPrepared = moviePlayer.isPrepared;

// get video current position time
double videoCurrentPosition = movieplayer.time;

// get video duration
float videoDuration = moviePlayer.length;

// the current frame index
int frameIndex = moviePlayer.frame;

// the total frames of the video
int totalFrames = moviePlayer.frameCount;
