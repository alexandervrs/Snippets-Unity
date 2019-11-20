
/**
 * Audio.cs
 * Audio related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.Audio; // for AudioMixerGroup


/*

	note: By default Audio in Unity games may lag and cause crackling. It is best to adjust
		  the DSP buffer size as needed and according to the audio filesizes & targets.
		  Go to Edit > Project Settings... > Audio
		  Under "DSP Buffer Size" option select either "Good Latency" or "Best Performance"

*/

/* -----------------------------------------
   Play Sound (Once)
----------------------------------------- */
AudioClip   soundClip;
AudioSource soundSource;

soundSource = gameObject.AddComponent<AudioSource>();
soundSource.PlayOneShot(soundClip);


/* -----------------------------------------
   Play Sound
----------------------------------------- */
AudioClip   soundClip;
AudioSource soundSource;

soundSource = gameObject.AddComponent<AudioSource>();

soundSource.clip      = soundClip;
soundSource.loop      = true; // play looping
soundSource.volume    = 1.0f; // 0.0f to 1.0f
soundSource.pitch     = 1.0f; // 0.0f to 3.0f
soundSource.panStereo = 0.0f; // (left) -1.0f to 1.0f (right)

soundSource.Play();


/* -----------------------------------------
	Play a Sound with Audio Mixer Group
----------------------------------------- */

/* 

    note: Create a AudioMixerGroup, Assets > Create > AudioMixer
          Open the AudioMixer and under "Groups" you can add AudioGroups to mass manipulate sounds
          e.g. useful for separating sounds to SFX/Music/Voice etc. or adding mass effects

          Right click on the "Volume" parameter of the mixer group and right click to expose it.
          At the top right of the Audio Mixer panel, click "Exposed Parameters" and you can rename it to e.g. "Volume".
          You can right click any parameter and expose it so you can manipulate it with audioMixer.SetFloat()/audioMixer.GetFloat()

*/

/// Class Body:
AudioClip       soundClip;
AudioSource     soundSource;
AudioMixer      soundMixer;
AudioMixerGroup mixerGroup;

// have a slider in the Inspector for the volume, to test
[Range(0.001f, 1)]
public float mixerVolume;

/// Start():

soundSource = gameObject.AddComponent<AudioSource>();

soundSource.clip      = soundClip;
soundSource.loop      = true; // play looping
soundSource.volume    = 1.0f; // 0.0f to 1.0f
soundSource.pitch     = 1.0f; // 0.0f to 3.0f
soundSource.panStereo = 0.0f; // (left) -1.0f to 1.0f (right)

// assign the target mixer group (e.g. with name "SoundEffects") to the AudioSource
mixerGroup = soundMixer.FindMatchingGroups("SoundEffects")[0];
soundSource.outputAudioMixerGroup = mixerGroup;

soundSource.Play();

// note: put the following in Update() to test the slider

// set the mixer volume (0 to 1)
mixerGroup.audioMixer.SetFloat("Volume", Mathf.Log((Mathf.Pow(10, 0 / 20) * mixerVolume))*20);

// get the mixer volume (0 to 1)
mixerGroup.audioMixer.GetFloat("Volume", out float outVolume);
outVolume = Mathf.Pow(10, outVolume / 20);
if (outVolume <= 0.001f) { outVolume = 0; } // remap to 0 if too low
Debug.Log("Volume is: "+outVolume);


/* -----------------------------------------
   Play Sound in 3D
----------------------------------------- */

// note: Audio file must be Mono format in order to be used as 3D sound

AudioClip   soundClip;
AudioSource soundSource;

soundSource = gameObject.AddComponent<AudioSource>();

soundSource.clip      = soundClip;
soundSource.loop      = true; // play looping
soundSource.volume    = 1.0f; // 0.0f to 1.0f
soundSource.pitch     = 1.0f; // 0.0f to 3.0f
soundSource.panStereo = 0.0f; // (left) -1.0f to 1.0f (right)

// 3D sound settings
soundSource.spatialBlend = 1; // 1 = 3D, 0 = 2D
soundSource.dopplerLevel = 0; // 0 to 5
soundSource.spread       = 0; // degrees 0 to 360
soundSource.minDistance  = 8;
soundSource.maxDistance  = 16;
soundSource.rolloffMode  = AudioRolloffMode.Linear; // or Logarithmic

soundSource.Play();


/* -----------------------------------------
   Play Sound Loop After Intro
----------------------------------------- */
/// Class Body:
public AudioClip soundIntro;
public AudioClip soundLoop;

private AudioSource soundIntroSource;
private AudioSource soundLoopSource;

/// Start(), Update():
AudioSource soundIntroSource = gameObject.AddComponent<AudioSource>();
AudioSource soundLoopSource  = gameObject.AddComponent<AudioSource>();

soundIntroSource.clip = soundIntro;

soundLoopSource.clip  = soundLoop;
soundLoopSource.loop  = true;

soundIntroSource.Play(); // play intro sound first once
soundLoopSource.PlayDelayed(soundIntroSource.clip.length); // when intro sound completes, play the loop part


/* -----------------------------------------
   Play Sound with Filters
----------------------------------------- */
/// Class Body:
public  AudioClip   soundClip;
private AudioSource soundSource;

private AudioLowPassFilter    lowPassFilter;
private AudioHighPassFilter   highPassFilter;
private AudioChorusFilter     chorusFilter;
private AudioEchoFilter       echoFilter;
private AudioReverbFilter     reverbFilter;
private AudioDistortionFilter distortionFilter;

/// Start():
soundSource    = gameObject.AddComponent<AudioSource>();
soundSource.bypassEffects = false; // set to true in order to ignore Filters

lowPassFilter    = gameObject.AddComponent<AudioLowPassFilter>();
highPassFilter   = gameObject.AddComponent<AudioHighPassFilter>();
reverbFilter     = gameObject.AddComponent<AudioReverbFilter>();
echoFilter       = gameObject.AddComponent<AudioEchoFilter>();
chorusFilter     = gameObject.AddComponent<AudioChorusFilter>();
distortionFilter = gameObject.AddComponent<AudioDistortionFilter>();

/// Start(), Update():
// High Pass Filter
highPassFilter.cutoffFrequency    = 10; // 10 (none) to 22000
highPassFilter.highpassResonanceQ = 0;

// Low Pass Filter
lowPassFilter.cutoffFrequency   = 22000;  // 10 to 22000 (none)
lowPassFilter.lowpassResonanceQ = 0;

// Distortion Filter
distortionFilter.distortionLevel = 0; // 0 to 1

// Reverb Filter
reverbFilter.dryLevel         = 0;  // -10000 to 0
reverbFilter.room             = -1000; // -10000 to 0
reverbFilter.roomHF           = -1200; // -10000 to 0
reverbFilter.roomLF           = 0; // -10000 to 0		
reverbFilter.decayTime        = 1.4f; // 0.1 to 20
reverbFilter.reflectionsLevel = -370; // -10000 to 1000
reverbFilter.reflectionsDelay = 0; // -10000 to 2000
reverbFilter.reverbLevel      = 1030f; // -10000 to 2000
reverbFilter.reverbDelay      = 0.011f; // 0 to 0.1
reverbFilter.decayHFRatio     = 0.54f; //0.1 to 2
reverbFilter.diffusion        = 100; // 0 to 100
reverbFilter.density          = 60f; // 0 to 100

// Echo Filter
echoFilter.delay      = 50;   // 10 to 5000
echoFilter.decayRatio = 0.1f; // 0 to 1
echoFilter.dryMix     = 1.0f;
echoFilter.wetMix     = 1.0f;

// Chorus Filter
chorusFilter.dryMix  = 0.5f;  // 0 to 1
chorusFilter.wetMix1 = 0.5f;  // 0 to 1
chorusFilter.wetMix2 = 0.5f;  // 0 to 1
chorusFilter.wetMix3 = 0.5f;  // 0 to 1
chorusFilter.delay   = 30f;   // 0.1 to 100
chorusFilter.rate    = 0.8f;  // 0 to 20
chorusFilter.depth   = 0.03f; // 0 to 1


/* -----------------------------------------
	Transition Mixer Snapshots
----------------------------------------- */
/// Class Body:
AudioMixer         soundMixer;
AudioMixerSnapshot snap;
AudioMixerSnapshot snap2;

/// Start(), MyMethod():
soundMixer.TransitionToSnapshots(new AudioMixerSnapshot[2] { snap, snap2 }, new float[2] { 0, 1 }, 2f);


/* -----------------------------------------
   Control Sound playback
----------------------------------------- */
// pause a sound
soundSource.Pause();

// resume a sound
soundSource.UnPause();

// stop a sound
soundSource.Stop();

// seek
soundSource.time = 1.3f;


/* -----------------------------------------
	Get Sound Info
----------------------------------------- */
// get current track position time
float trackPosition = soundSource.time;

// get track duration
float trackDuration = soundSource.clip.length;


/* -----------------------------------------
	Stop All Sounds
----------------------------------------- */
AudioSource[] soundSources;
soundSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
foreach(AudioSource sound in soundSources) {
	sound.Stop();
}


/* -----------------------------------------
	Change Master Volume
----------------------------------------- */
AudioListener.volume = 0.5f;
