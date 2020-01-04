
/**
 * Plugin.cs
 * External Native or Managed Library-related snippets for Unity
 */

/* using */
using System.Runtime.InteropServices; // for DllImport


/* -----------------------------------------
   Call Native DLL Plugin (Windows)
----------------------------------------- */

/*
	
	1. Compile main.cpp source to two .dll files, one compiled for 32-bit architecture and one for 64-bit

*/

// -------------( main.cpp )--------------

#ifndef UNICODE
	#undef _UNICODE
#else
	#ifndef _UNICODE
		#define _UNICODE
	#endif
#endif

#define export extern "C" __declspec (dllexport)

#include <windows.h>

export void MyPluginFunction(LPCWSTR text) 
{
	
	MessageBox(NULL, text, L"Message", MB_OK | MB_ICONINFORMATION);
	
}

// -------------( main.cpp )--------------

/*

	2. Place the .dll files into their respective /x86/ and /x64/ folders under "Plugins" special folder
	   e.g. "Assets/Plugins/MyDLL/x86/test.dll" for the 32-bit file, "Assets/Plugins/MyDLL/x64/test.dll" for the 64-bit file

	3. Click on the dll files in the Unity File Manager, in the Inspector set each of them to be for "Standalone" and 
       their respective architectures (x86 is for 32-bit, x86_64 for 64-bit)

*/

/* 4. Call the DLL from C# Script */

// DLL are only for the Windows platform
#if UNITY_STANDALONE_WIN

// Constructor:
[DllImport("test.dll", CharSet = CharSet.Unicode)] // load test.dll from "Plugins" folder, Unity will take care of which dll version to load
public static extern double MyPluginFunction(string text); // define the method "MyPluginFunction()" from the .dll which takes one string argument

/// Awake(), Start(), Update():
MyPluginFunction("Test"); // call the method from the DLL

#endif


/* -----------------------------------------
   Call Native JAR/AAR Plugin (Android)
----------------------------------------- */

/*
	
	1. Compile AndroidPlugin.java source to a .jar/.aar file

*/

// -------------( AndroidPlugin.java )--------------

package com.test.AndroidPlugin;

import android.os.Environment;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.Context;

public class AndroidPlugin
{

	private Context context;

	public AndroidPlugin(Context context)
	{
		this.context = context;
	}

	public String MyPluginFunction(String message)
    {
		return "From Android Plugin: "+message;
	}

}

// -------------( AndroidPlugin.java )--------------

/*

	2. Place the .jar file into "Plugins" special folder
	   e.g. "Assets/Plugins/MyJAR/test.jar"

	3. Click on the jar/aar file in the Unity File Manager, in the Inspector make sure it's marked only for Android

*/

/* 4. Call the JAR/AAR from C# Script */

// JAR/AAR are only for the Android platform
#if UNITY_ANDROID

/// Awake(), Start(), Update():

// get UnityPlayer main class and current activity
AndroidJavaClass unityPlayer      = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

// load "com.test.AndroidPlugin" class from test.jar from "Plugins" folder
AndroidJavaObject androidPluginClass = new AndroidJavaObject("com.test.AndroidPlugin", currentActivity);

// call the MyPluginFunction() from "com.test.AndroidPlugin" with first argument as string, returns back a string
androidPluginClass.Call<string>("MyPluginFunction", new object[] { "Test" });

#endif


/* -----------------------------------------
   Call HTML5 Plugin (WebGL)
----------------------------------------- */

/*
	
	1. Create a file with the extension .jslib, write the code in Javascript
	2. Place the .jslib file into "Plugins" special folder
	   e.g. "Assets/Plugins/MyJSLIB/test.jslib"

*/

// -------------( test.jslib )--------------

mergeInto(LibraryManager.library, {

	MyPluginFunction: function(str) {
		window.alert(Pointer_stringify(str));
	}

});

// -------------( test.jslib )--------------

/* 

	3. Click on the jslib file in the Unity File Manager, in the Inspector make sure it's marked only for WebGL 

	4. Call the JSLIB from C# Script 

*/

// JSLIB are only for the WebGL platform
#if UNITY_WEBGL

// Constructor:
[DllImport("__Internal")] // load from .jslib plugin
private static extern void MyPluginFunction(string str); // define the method "MyPluginFunction()" in the .jslib

/// Awake(), Start(), Update():
MyPluginFunction("Some Text"); // call the method from the JSLIB

#endif


/* -----------------------------------------
   Call System DLL (Windows)
----------------------------------------- */

/* You can also call any system Windows DLL, doesn't have to be in "Plugins" folder */

// Constructor:

// DLLs are only for the Windows platform
#if UNITY_STANDALONE_WIN

// Unity cannot give back the Window Handle (hWnd), so we need to call WINAPI
[DllImport("user32.dll")]
public static extern System.IntPtr GetActiveWindow();

[DllImport("WindowPlugin")] // load WindowPlugin.dll from "Plugins" folder
public static extern float MyPluginFunction(IntPtr hWnd); // call WindowPlugin which receives a hWnd IntPtr parameter and returns a float


/// Awake(), Start(), Update():

// call the function and pass the active window passed from WINAPI
MyPluginFunction(GetActiveWindow());

#endif

