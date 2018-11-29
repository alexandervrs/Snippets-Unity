
/**
 * Audio.cs
 * Audio related snippets for Unity
 */

/* using */
using UnityEngine;


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

