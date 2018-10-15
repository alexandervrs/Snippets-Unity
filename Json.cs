
/**
 * Json.cs
 * Json related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Load Json as Object
----------------------------------------- */

// create a class first (outside main class)
[System.Serializable]
public class Settings
{
    public SettingsVideo video;
	/// more ...
}

// create the child classes (outside main class)
[System.Serializable]
public class SettingsVideo
{
    // class SettingsVideo is a child to Settings
	public string fullscreen;
    public string vsync;
	// more ...
}

/// more ...



// load json data (from inside method)
SettingsObj = JsonUtility.FromJson<Settings>("{\"video\":{ \"fullscreen\":\"1\", \"vsync\": \"1\" }}");
bool isFullscreen = (SettingsObj.video.fullscreen).Equals("1");
if (isFullscreen) {
	// fullscreen is enabled in Settings ...
}

