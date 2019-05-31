
/**
 * Editor.Dialog.cs
 * Editor Dialog related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;
using System.Reflection; // for Application, MethodInfo, BindingFlags only


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Show MessageBox
----------------------------------------- */
// info
EditorUtility.DisplayDialog("Info", "Action taken!", "OK");

// question
if (EditorUtility.DisplayDialog("Warning", "Do you want to take this action?", "Yes", "No")) {
	// user selected "Yes" ...
}


/* -----------------------------------------
   Show Object Picker Dialog
----------------------------------------- */
/// Class Body:
Object selectedObject = null;

/// OnGUI:
if (GUILayout.Button("ShowObjectPicker")) {
	int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
	EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, "", controlID); // "true" is allowSceneObjects
}

switch (Event.current.commandName) {

	case "ObjectSelectorUpdated":
		selectedObject = EditorGUIUtility.GetObjectPickerObject();
		Repaint();
		break;

	case "ObjectSelectorClosed":
		selectedObject = EditorGUIUtility.GetObjectPickerObject();
		Repaint();
		break;

}

// reflect the changes in an ObjectField
EditorGUILayout.ObjectField(selectedObject, typeof(Sprite), true); // "true" is allowSceneObjects


/* -----------------------------------------
   Show File Open Dialog
----------------------------------------- */
// open file dialog (open image as Texture2D)
string path = EditorUtility.OpenFilePanelWithFilters("File Open", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), new string[]{"All Supported Images", "png,jpg,gif", "PNG Images", "png", "JPG Images", "jpg", "GIF Images", "gif"});

if (!string.IsNullOrEmpty(path)) {

	string name              = System.IO.Path.GetFileName( path );
	string selectedPath      = AssetDatabase.GetAssetPath(Selection.activeObject);
	string originalSelection = selectedPath;

	if (System.IO.File.Exists(selectedPath)) {
		selectedPath = System.IO.Path.GetDirectoryName(selectedPath);
	}
	if (string.IsNullOrEmpty(selectedPath)) {
		selectedPath = "Assets";
	}

	selectedPath += "/";

	string assetPath       = selectedPath+name;
	string destinationPath = selectedPath + name;

	Debug.Log("Original Selection is: "+originalSelection);
	Debug.Log("Copying file from: "+path);
	Debug.Log("Saving under: "+destinationPath);
	Debug.Log("Importing under folder: "+selectedPath);
	Debug.Log("Importing as: "+assetPath);
	
	Texture2D testAsset = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
	if (testAsset != null) {

		if (AssetDatabase.Contains(testAsset)) {
			Debug.Log("File "+destinationPath+" already exists, removing...");
			AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(testAsset));
			AssetDatabase.Refresh();
		}

	}

	System.IO.File.Copy( path, destinationPath );
	AssetDatabase.Refresh();

	TextureImporter importer = TextureImporter.GetAtPath( assetPath ) as TextureImporter;
	TextureImporterSettings settings = new TextureImporterSettings();

	importer.ReadTextureSettings( settings );
	
	settings.textureType                        = TextureImporterType.Sprite; // Image, Sprite, NormalMap, Bump ... 
	settings.spriteMeshType                     = SpriteMeshType.FullRect;    // FullRect, Tight
	settings.spritePivot                        = new Vector2(0.5f, 0.5f);
	settings.spriteBorder                       = new Vector4(4, 4, 4, 4);
	settings.spritePixelsPerUnit                = 100;
	settings.spriteGenerateFallbackPhysicsShape = false;
	settings.filterMode                         = FilterMode.Trilinear;       // Bilinear, Trilinear, Point
	settings.wrapMode                           = TextureWrapMode.Clamp;      // Repeat, Mirror, MirrorOnce
	settings.alphaIsTransparency                = true;
	settings.readable                           = true;
	settings.mipmapEnabled                      = false;

	importer.SetTextureSettings( settings );

	AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
	AssetDatabase.Refresh();

	Debug.Log("Imported Texture \""+name+"\" to: "+assetPath);

}

// open file dialog (open audio as AudioClip)
string path = EditorUtility.OpenFilePanelWithFilters("File Open", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), new string[]{"All Supported Audio", "wav,mp3,ogg", "WAV Audio", "wav", "MP3 Audio", "mp3", "OGG Audio", "ogg"});

if (!string.IsNullOrEmpty(path)) {

	string name              = System.IO.Path.GetFileName( path );
	string selectedPath      = AssetDatabase.GetAssetPath(Selection.activeObject);
	string originalSelection = selectedPath;

	if (System.IO.File.Exists(selectedPath)) {
		selectedPath = System.IO.Path.GetDirectoryName(selectedPath);
	}
	if (string.IsNullOrEmpty(selectedPath)) {
		selectedPath = "Assets";
	}

	selectedPath += "/";

	string assetPath       = selectedPath+name;
	string destinationPath = selectedPath + name;

	Debug.Log("Original Selection is: "+originalSelection);
	Debug.Log("Copying file from: "+path);
	Debug.Log("Saving under: "+destinationPath);
	Debug.Log("Importing under folder: "+selectedPath);
	Debug.Log("Importing as: "+assetPath);
	
	AudioClip testAsset = (AudioClip)AssetDatabase.LoadAssetAtPath(assetPath, typeof(AudioClip));
	if (testAsset != null) {

		if (AssetDatabase.Contains(testAsset)) {
			Debug.Log("File "+destinationPath+" already exists, removing...");
			AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(testAsset));
			AssetDatabase.Refresh();
		}

	}

	System.IO.File.Copy( path, destinationPath );
	AssetDatabase.Refresh();

	AudioImporter importer = AudioImporter.GetAtPath( assetPath ) as AudioImporter;
	AudioImporterSampleSettings settings = new AudioImporterSampleSettings();

	string platform = "Standalone"; // platforms can be "Standalone", "iOS", "Android", "WebGL", "PS4", "PSP2", "XBoxOne"

	importer.GetOverrideSampleSettings(platform);

	settings.quality           = 1.0f; // (low) 0 to 1 (high)
	settings.loadType          = AudioClipLoadType.DecompressOnLoad; //CompressedInMemory, Streaming
	settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;
	settings.compressionFormat = AudioCompressionFormat.Vorbis;

	importer.preloadAudioData = false;
	importer.ambisonic        = false;
	importer.forceToMono      = false;
	importer.loadInBackground = false;
	
	importer.SetOverrideSampleSettings(platform, settings);

	AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
	AssetDatabase.Refresh();

	Debug.Log("Imported Audio \""+name+"\" to: "+assetPath);


}

// open file dialog (open video as VideoClip)
string path = EditorUtility.OpenFilePanelWithFilters("File Open", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), new string[]{ "All Supported Video", "mp4,webm,mov,avi", "MP4 Video", "mp4", "Webm Video", "webm", "Mov Video", "mov", "AVI Video", "avi" });

if (!string.IsNullOrEmpty(path)) {

	string name              = System.IO.Path.GetFileName( path );
	string selectedPath      = AssetDatabase.GetAssetPath(Selection.activeObject);
	string originalSelection = selectedPath;

	if (System.IO.File.Exists(selectedPath)) {
		selectedPath = System.IO.Path.GetDirectoryName(selectedPath);
	}
	if (string.IsNullOrEmpty(selectedPath)) {
		selectedPath = "Assets";
	}

	selectedPath += "/";

	string assetPath       = selectedPath+name;
	string destinationPath = selectedPath + name;

	Debug.Log("Original Selection is: "+originalSelection);
	Debug.Log("Copying file from: "+path);
	Debug.Log("Saving under: "+destinationPath);
	Debug.Log("Importing under folder: "+selectedPath);
	Debug.Log("Importing as: "+assetPath);
	
	VideoClip testAsset = (VideoClip)AssetDatabase.LoadAssetAtPath(assetPath, typeof(VideoClip));
	if (testAsset != null) {

		if (AssetDatabase.Contains(testAsset)) {
			Debug.Log("File "+destinationPath+" already exists, removing...");
			AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(testAsset));
			AssetDatabase.Refresh();
		}

	}

	System.IO.File.Copy( path, destinationPath );
	AssetDatabase.Refresh();

	VideoClipImporter importer = AudioImporter.GetAtPath( assetPath ) as VideoClipImporter;
	VideoImporterTargetSettings settings = new VideoImporterTargetSettings();

	string platform = "Standalone"; // platforms can be "Standalone", "iOS", "Android", "WebGL", "PS4", "PSP2", "XBoxOne"

	importer.GetTargetSettings(platform);

	settings.codec             = VideoCodec.Auto;
	settings.enableTranscoding = true;
	settings.spatialQuality    = VideoSpatialQuality.HighSpatialQuality;
	
	importer.importAudio = true;
	importer.keepAlpha   = true;

	importer.SetTargetSettings(platform, settings);

	AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
	AssetDatabase.Refresh();

	Debug.Log("Imported Video \""+name+"\" to: "+assetPath+", "+importer.frameCount+" frames");

}

// open file dialog (load shader & create material for it)
string path = EditorUtility.OpenFilePanelWithFilters("File Open", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), new string[]{"Shader", "shader"});

if (!string.IsNullOrEmpty(path)) {

	string name              = System.IO.Path.GetFileName( path );
	string selectedPath      = AssetDatabase.GetAssetPath(Selection.activeObject);
	string originalSelection = selectedPath;

	if (System.IO.File.Exists(selectedPath)) {
		selectedPath = System.IO.Path.GetDirectoryName(selectedPath);
	}
	if (string.IsNullOrEmpty(selectedPath)) {
		selectedPath = "Assets";
	}

	selectedPath += "/";

	string assetPath       = selectedPath+name;
	string destinationPath = selectedPath + name;
	string materialName    = selectedPath+(System.IO.Path.GetFileNameWithoutExtension( assetPath )) + ".mat";

	Debug.Log("Original Selection is: "+originalSelection);
	Debug.Log("Copying file from: "+path);
	Debug.Log("Saving under: "+destinationPath);
	Debug.Log("Importing under folder: "+selectedPath);
	Debug.Log("Importing as: "+assetPath);
	Debug.Log("Material will be saved as: "+materialName);
	
	Shader testAsset = (Shader)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Shader));
	if (testAsset != null) {

		if (AssetDatabase.Contains(testAsset)) {
			Debug.Log("File "+destinationPath+" already exists, removing...");
			AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(testAsset));
			AssetDatabase.Refresh();
		}

	}

	System.IO.File.Copy( path, destinationPath );
	AssetDatabase.Refresh();

	AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
	AssetDatabase.Refresh();

	Shader assetName = (Shader)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Shader));
	string shaderName = assetName.name;

	Material mat = new Material( Shader.Find(shaderName) );
	if (!AssetDatabase.Contains(mat)) {
		AssetDatabase.CreateAsset( mat, materialName );
		Debug.Log("Imported Shader \""+name+"\" to: "+assetPath+" and created Material for it");
	} else {
		
		Debug.Log("Imported Shader \""+name+"\" to: "+assetPath+". Material already exists");
	}

	AssetDatabase.Refresh();

}

// open folder dialog (save selected Textures to folder)
Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Unfiltered);
if (textures.Length < 1) {
	Debug.Log("No textures selected");
	return;
}

string path = EditorUtility.SaveFolderPanel("Export to Folder", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "");

if (path.Length != 0) {

	foreach (Texture2D texture in textures) {

		byte[] imageData = texture.EncodeToPNG();

		if (imageData != null) {
			System.IO.File.WriteAllBytes(path + "/" + texture.name + ".png", imageData);
			Debug.Log("Saving texture to "+path + "/" + texture.name + ".png");
		} else {
			Debug.Log("Could not convert " + texture.name + " to png. Skipping...");
		}
	}

	AssetDatabase.Refresh();

}

// file save dialog
Object testAsset = (Object)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject), typeof(Object));

string path = EditorUtility.SaveFilePanel("File Save", System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), testAsset.name, System.IO.Path.GetExtension( AssetDatabase.GetAssetPath(Selection.activeObject )).Replace(".", ""));

if (!string.IsNullOrEmpty(path)) {
	System.IO.File.Copy(AssetDatabase.GetAssetPath(Selection.activeObject), path);
	Debug.Log("File copied");
	/// work with the target filepath ...
}


/* -----------------------------------------
   Show Notification
----------------------------------------- */
// show notification in the Game preview window
EditorWindow[] editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
foreach(EditorWindow win in editorWindows) {
	if (win.titleContent.text.Equals("Game")) {
		win.ShowNotification(new GUIContent("Sample Notification"));
	}
}

// remove the notification from the Game preview window
EditorWindow[] editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
foreach(EditorWindow win in editorWindows) {
	if (win.titleContent.text.Equals("Game")) {
		win.RemoveNotification();
	}
}

