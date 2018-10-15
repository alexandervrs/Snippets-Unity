
/**
 * Json.cs
 * Json related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Setup Json Object
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


/* -----------------------------------------
   Load Json as Object
----------------------------------------- */
// note: requires a suitable Object (see "Setup Json Object" above)

// load json data (from inside method)
Settings settingsData = JsonUtility.FromJson<Settings>("{\"video\":{ \"fullscreen\":\"1\", \"vsync\": \"1\" }}");

bool isFullscreen = (settingsData.video.fullscreen).Equals("1");
if (isFullscreen) {
	// fullscreen is enabled in Settings ...
}


// OR load json data from Resources folder (as TextAsset text)

// -------------( AppSettings.json )--------------
{
	"video":{
		"fullscreen":"1",
		"vsync": "1"
	}
}
// -------------( AppSettings.json )--------------

TextAsset textAsset   = Resources.Load<TextAsset>("AppSettings");
Settings settingsData = JsonUtility.FromJson<Settings>(textAsset.text);

bool isFullscreen = (settingsData.video.fullscreen).Equals("1");
if (isFullscreen) {
	// fullscreen is enabled in Settings ...
}


/* -----------------------------------------
   Save Object data as Json
----------------------------------------- */
// note: requires a suitable Object (see "Setup Json Object" above)

Settings settingsData = new Settings();

// change settings values under "video" section
settingsData.video.fullscreen = "0";
settingsData.video.vsync      = "1";

string jsonFormattedString = JsonUtility.ToJson(settingsData);
// save jsonFormattedString to file etc. ...

