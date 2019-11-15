
/**
 * Compiler.cs
 * Compiler related snippets for Unity
 */

/* using */
using UnityEngine;
 

/* -----------------------------------------
   Custom define values
----------------------------------------- */

/*

	1. Go to Project Settings > Player and choose a platform
	2. Look for "Scripting Define Symbols" field
	3. Type in your custom defines separated by semicolon ;
	   e.g. VERSION_FOR_APPSTORE
	4. then anywhere in your Scripts you can use:
	
*/

#if VERSION_FOR_APPSTORE
Debug.Log("This code only compiles and executes when VERSION_FOR_APPSTORE is defined");
#else
Debug.Log("This code only compiles and executes when VERSION_FOR_APPSTORE is NOT defined");
#endif
 
/* -----------------------------------------
   Compile the code on specific platform
----------------------------------------- */

#if UNITY_STANDALONE_WIN
Debug.Log("Platform is Windows");
#endif

#if UNITY_STANDALONE_OSX
Debug.Log("Platform is macOS");
#endif

#if UNITY_STANDALONE_LINUX
Debug.Log("Platform is Linux");
#endif

#if UNITY_STANDALONE
Debug.Log("Platform is Desktop (Windows, macOS or Linux)");
#endif

#if UNITY_ANDROID
Debug.Log("Platform is Android");
#endif

#if UNITY_IOS
Debug.Log("Platform is iOS");
#endif

#if UNITY_TVOS
Debug.Log("Platform is tvOS");
#endif

#if UNITY_PS4
Debug.Log("Platform is Playstation 4");
#endif

#if UNITY_XBOXONE
Debug.Log("Platform is Xbox One");
#endif

#if UNITY_WSA
Debug.Log("Platform is Windows UWP");
#endif

#if UNITY_WEBGL
Debug.Log("Platform is WebGL");
#endif

#if UNITY_STANDALONE_WIN && UNITY_FACEBOOK
Debug.Log("Platform is Windows (Facebook)");
#endif

#if UNITY_WEBGL && UNITY_FACEBOOK
Debug.Log("Platform is WebGL (Facebook)");
#endif

#if UNITY_EDITOR
Debug.Log("Platform is Unity Editor");
#endif

#if UNITY_EDITOR_WIN
Debug.Log("Platform is Unity Editor (Windows)");
#endif

#if UNITY_EDITOR_OSX
Debug.Log("Platform is Unity Editor (macOS)");
#endif
