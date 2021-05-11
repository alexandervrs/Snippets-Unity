
/**
 * Timeline.cs
 * Timeline & Playable related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.Playables; // for PlayableDirector, PlayableGraph, DirectorUpdateMode

/* -----------------------------------------
   Setup Timeline
----------------------------------------- */

/*

    1. Window > Sequencing > Timeline
    2. Assets > Create > Timeline to create a new Timeline Asset
    3. Create/Choose a GameObject & add a "Playable Director" Component to it
    4. For the Playable Director component, change "Playable" and select the Timeline Asset for it
    5. Disable "Play On Awake" if you want to start the Timeline via a Script instead
    6. Switch to the Timeline window, and click on the "+" button to add some Tracks to the Timeline

    note: If you add an AnimationClip & need it to animate relatively from its current Scene position
          then click on the Animation track, and in the Inspector change "Track Offsets" to "Apply Scene Offsets".
          Then drag and drop the Animation on the track, click on it & from the Inspector again uncheck "Remove Start Offset"

    note: To make a ParticleSystem play, add an ActivationTrack & choose the GameObject with the ParticleSystem, when the
          playhead goes through the "Active" segment, then the ParticleSystem will play (once or loop depending on the ParticleSystem settings).
          If you want to smoothly Stop() a ParticleSystem then you either have to account for the lifetime to cover the duration 
          or use an AnimationClip with a Animation Event that stops that Particle System via code.

*/


/* -----------------------------------------
   Get Playable Component & PlayableGraph
----------------------------------------- */
/// Class Body:
PlayableDirector director;
PlayableGraph playableGraph;

/// Start():
director = gameObject.GetComponent<PlayableDirector>();
playableGraph = director.playableGraph;


/* -----------------------------------------
   Playable Director Playback
----------------------------------------- */

// play the timeline asset with the director component
director.Play();

// stop the timeline asset playback
director.Stop();

// pause the timeline asset playback
director.Pause();

// resume the timeline asset playback
director.Resume();

// refresh changes done to the timeline, can be used to refresh the animation of the timeline after a seek operation
director.RebuildGraph();

// disable automatic playback on start (or change the "Play On Awake" setting on the Component Inspector itself)
director.playOnAwake = false;

// change from where the Timeline will start playing from
director.initialTime = 2.0d; // start from 2 seconds (120 frames)

// get the current position of the playhead
double currentTime = director.time;

// seek/set the current position of the playhead
director.time = 2.0d; // go to 2 seconds (120 frames)

// check if timeline playback is paused
if (playableDirector.state == PlayState.Paused) {
    // timeline is paused ...
}

// check if timeline playback is stopped
if (playableDirector.state != PlayState.Playing) {
    // timeline is stopped ...
}


/* -----------------------------------------
   Manually Update Director Playback
----------------------------------------- */

/*
    
    note: if you manually update the Director playback, Signals and Audio Tracks will not work.
        Instead of Signals, you can use an AnimationClip that contains an Animation Event (See Animation.cs).
          if you want Audio cues to still play, then use an Activation Track & select a GameObject with an attached
          AudioSource & make sure "Play On Awake" option is enabled. Otherwise you can use an AnimationClip with Animation Event
          that triggers a sound.
          

*/

/// Start():
director.timeUpdateMode = DirectorUpdateMode.Manual; // or change the "Update Method" setting on the Component Inspector itself to "Manual"

// note: use LateUpdate() instead of Update(), otherwise AnimationClips in the Timeline will not play correctly

/// LateUpdate():
if (playableGraph.IsValid()) {
    playableGraph.Evaluate(Time.deltaTime); // you may use your own deltaTime, e.g. if you want to manually limit FPS
}
