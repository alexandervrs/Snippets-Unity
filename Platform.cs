
/**
 * Platform.cs
 * Platform-specific compilation related snippets for Unity
 */

/* using */
using UnityEngine;
 

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