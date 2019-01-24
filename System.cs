
/**
 * System.cs
 * System related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.Scripting; // for GarbageCollector
#if UNITY_ANDROID
using UnityEngine.Android; // for Permission
#endif


/* -----------------------------------------
   Get System Info
----------------------------------------- */
// get the battery level, from 0(empty) to 1(full), returns -1 when not supported
float batteryPower = SystemInfo.batteryLevel;

// returns if the device supports vibration
bool hasVibration = SystemInfo.supportsVibration;

// returns if the device has accelerometer
bool hasAccelerometer = SystemInfo.supportsAccelerometer;

// returns if the device has gyroscope
bool hasGyroscope = SystemInfo.supportsGyroscope;

// return if audio playback is enabled
bool hasAudio = SystemInfo.supportsAudio;

// get the device type
if (SystemInfo.deviceType == DeviceType.Desktop) { // DeviceType.Desktop, DeviceType.Handheld, DeviceType.Console
	// is Desktop (Windows, macOS, Linux) ...
}

// get pixel shader level
/*

	Values: 
	50 Shader Model 5.0 (DX11.0) 
	46 OpenGL 4.1 capabilities (Shader Model 4.0 + tessellation) 
	45 Metal / OpenGL ES 3.1 capabilities (Shader Model 3.5 + compute shaders) 
	40 Shader Model 4.0 (DX10.0) 
	35 OpenGL ES 3.0 capabilities (Shader Model 3.0 + integers, texture arrays, instancing) 
	30 Shader Model 3.0 
	25 Shader Model 2.5 (DX11 feature level 9.3 feature set) 
	20 Shader Model 2.0.

*/
if (SystemInfo.graphicsShaderLevel >= 30) {
	// minimum shader level detected ...
}

// get platform / OS
/*

	OSXEditor // Editor
	OSXPlayer
	WindowsPlayer
	WindowsEditor // Editor
	IPhonePlayer
	Android
	LinuxPlayer
	LinuxEditor // Editor
	WebGLPlayer
	WSAPlayerX86	// Windows Store App, CPU architecture is X86
	WSAPlayerX64	// Windows Store App, CPU architecture is X64
	WSAPlayerARM	// Windows Store App, CPU architecture is ARM
	PSP2	// (PS Vita)
	PS4
	XboxOne
	tvOS
	Switch

*/
if (Application.platform == RuntimePlatform.WindowsPlayer) {
	// running on Windows ...
}

// get API level (Android), method 1
#if UNITY_ANDROID
int apiLevel = int.Parse(SystemInfo.operatingSystem.Substring(SystemInfo.operatingSystem.IndexOf("-")+ 1, 3));
#endif

// get API level (Android), method 2
#if UNITY_ANDROID
int apiLevel = -1;
using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
	apiLevel = version.GetStatic<int>("SDK_INT");
}
#endif


/* -----------------------------------------
   Permissions (Android, Unity 2018.3+)
----------------------------------------- */

/*
	note: By default Unity asks for all the permissions required on App startup but it may be better UX to ask for a
          specific permission only when your App needs it. You can add: 
			<meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="true" />
		  metadata to your Android manifest in "<ProjectRoot>/Assets/Plugins/Android/AndroidManifest.xml" to suppress
		  the permission dialogs shown at the start of the game
*/

// request Permission to use a feature
/*
	Permissions available:
	
		CoarseLocation
		FineLocation
		Microphone
		Camera
		ExternalStorageRead
		ExternalStorageWrite
		
*/
#if UNITY_ANDROID
if (!Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
	Permission.RequestUserPermission(Permission.Microphone);
}
#endif

// check if the user has permitted a feature to be used
#if UNITY_ANDROID
if (Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
	// microphone is available ...
} else {
	// microphone is not available, ask for permission, explain why you need the permission to the user etc.
}
#endif

/*

	Permissions added by Unity:
	
	Network classes need the INTERNET permission
	Vibration (such as Handheld.Vibrate) needs VIBRATE
	The InternetReachability property needs ACCESS_NETWORK_STATE
	Location APIs (such as LocationService) needs ACCESS_FINE_LOCATION
	WebCamTexture APIs needs CAMERA permission
	The Microphone class needs RECORD_AUDIO
	NetworkDiscovery and NetworkTransport.SetMulticastLock need CHANGE_WIFI_MULTICAST_STATE
	
	Custom permission added to manifest like:
		<uses-permission android:name="android.permission.INTERNET" />

*/


/* -----------------------------------------
   Garbage Collector (Unity 2018.3+)
----------------------------------------- */

/*
	note: Disabling the Garbage Collector must be done with great care as continuous memory allocations will
	      increase memory usage and will not free any memory. 
		  Disabling the Garbage Collector can help with random hiccups during the game that is caused by the GC randomly 
		  called by the application. The Garbage Collector can be disabled, any memory allocated ahead of time (e.g. object pooling) 
		  and the GC enabled back along with calling System.GC.Collect() manually to free up memory (usually at the end of a level/Scene)
	
*/

// enable the Garbage Collector
GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;

// disable the Garbage Collector
GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;

// listen to Garbage Collector changed event
GarbageCollector.GCModeChanged += (GarbageCollector.Mode mode) =>
{
	Debug.Log("GCModeChanged: "+mode);
};

// manually do a Garbage Collect 
// note: GarbageCollector.GCMode MUST be enabled first with GarbageCollector.GCMode = GarbageCollector.Mode.Enabled
//       otherwise System.GC.Collect() will not have any effect, it can be disabled back afterwards)
System.GC.Collect();

