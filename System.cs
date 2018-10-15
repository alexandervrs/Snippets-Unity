
/**
 * System.cs
 * System related snippets for Unity
 */

/* using */
using UnityEngine;


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