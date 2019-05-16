
/**
 * Editor.cs
 * Editor related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEditor.SceneManagement; // for EditorSceneManager
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
   Inspector Custom Editor
----------------------------------------- */

// -------------( SampleCustomEditor.cs )--------------

using UnityEngine;
using UnityEditor;

// note: change MyComponent to an existing Component type e.g. SpriteRenderer or custom Script (e.g. SampleCustomComponent)

[CustomEditor(typeof(SampleCustomComponent))]
public class SampleCustomEditor : Editor
{
	
	public override void OnInspectorGUI()
    {

		// set the component target that the additional GUI elements will appear to
		SampleCustomComponent targetComponent = (SampleCustomComponent)target;
		
		// draws the default Inspector items
		DrawDefaultInspector();

		if (GUILayout.Button("Test")) {
			//targetComponent.MyMethod(); // call a "MyMethod()" from targetComponent
			Debug.Log("Button Clicked...");
		}

	}

}

// -------------( SampleCustomEditor.cs )--------------

// target the specific Component Inspector
SampleCustomComponent targetComponent = (SampleCustomComponent)target;

// draws all the current Inspector items taken from the Component
DrawDefaultInspector();


/* --------------------------------------------
   Get Data from Default Inspector Controls
-------------------------------------------- */

/* 		note: The following is the way to show UI controls from the Default Inspector, in
			  in case you're not using DrawDefaultInspector();
*/

/* serialize the Inspector public variables */
/// Class Body:
private SerializedObject m_object;

/// OnEnable():
m_object = new SerializedObject(targets);

/// OnInspectorGUI():
// update the Inspector properties/variables
m_object.ApplyModifiedProperties();

// add a UI control based on the variable type
EditorGUILayout.PropertyField(m_object.FindProperty("somePublicVariable"));


/* -----------------------------------------
   Inspector Custom UI Controls
----------------------------------------- */

/* ----- (SEPARATOR) add a Separator to the Inspector ----- */
GUIStyle guiSeparator       = new GUIStyle("box");
guiSeparator.border.top     = guiSeparator.border.bottom  = 1;
guiSeparator.margin.top     = guiSeparator.margin.bottom  = 5;
guiSeparator.margin.bottom  = guiSeparator.margin.top     = 5;
guiSeparator.padding.top    = guiSeparator.padding.bottom = 1;
GUILayout.Box("", guiSeparator, GUILayout.ExpandWidth(true), GUILayout.Height(1));

/* ----- (SPACE) add 20 pixels Space to the Inspector ----- */
GUILayout.Space(20);

/* ----- (LABEL) add a Text Label to the Inspector ----- */
GUILayout.Label("Some Text");

// OR centered
GUIStyle guiLabelCentered  = new GUIStyle("label");
guiLabelCentered.alignment = TextAnchor.UpperCenter;
GUILayout.Label("Some Text", guiLabelCentered, GUILayout.ExpandWidth(true));

/* ----- (LABEL) add an Image Label to the Inspector ----- */
GUILayout.Label(Resources.Load<Texture2D>("myImage"));

// OR centered
GUIStyle guiLabelCentered  = new GUIStyle("label");
guiLabelCentered.alignment = TextAnchor.UpperCenter;
GUILayout.Label(Resources.Load<Texture2D>("editor_path_icon"), guiLabelCentered, GUILayout.ExpandWidth(true));

/* ----- (LABEL) add a Header style text to the Inspector ----- */
GUIStyle guiLabelCentered  = new GUIStyle("label");
guiLabelCentered.alignment = TextAnchor.MiddleLeft;
guiLabelCentered.fontStyle = FontStyle.Bold;
GUILayout.Label("My Header", guiLabelCentered, GUILayout.ExpandWidth(true));

/* ----- (BUTTON) add a Button to the Inspector ----- */
if (GUILayout.Button("Test"), GUI.skin.button, GUILayout.ExpandWidth(true)) { // note: GUI.skin.button loads the default UI style
	Debug.Log("Button Clicked...");
}

/* ----- (BUTTON) add a 32x32 Image Button to the Inspector ----- */
if (GUILayout.Button(Resources.Load<Texture2D>("myIcon"), GUI.skin.button, GUILayout.Width(32), GUILayout.Height(32))) {
	Debug.Log("Button Clicked...");
}

/* ----- (TEXTFIELD) add a Textfield to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI(), OnGUI():
stringResult = GUILayout.TextField(stringResult, 32); // 32 is max Characters

/* ----- (TEXTFIELD) add a Label & Textfield to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI(), OnGUI():
// reset the fields to look like Inspector
EditorGUIUtility.labelWidth = 0;
EditorGUIUtility.fieldWidth = 0;

stringResult = EditorGUILayout.TextField("Label Text", stringResult);

// OR with a Tooltip
stringResult = EditorGUILayout.TextField(new GUIContent("Label Text", "Tooltip Text"), stringResult);

// OR with Tooltip and Icon
stringResult = EditorGUILayout.TextField(new GUIContent("Label Text", Resources.Load<Texture2D>("myIcon"), "Tooltip Text"), stringResult);

/* ----- (NUMBER) add a Numeric field to the Inspector ----- */
/// Class Body:
private int myNumberInt = 5;
private float myNumberFloat = 5.4;

/// OnInspectorGUI(), OnGUI():
myNumberInt = EditorGUILayout.IntField("Label Text", myNumberInt);
myNumberFloat = EditorGUILayout.FloatField("Label Text", myNumberFloat);

/* ----- (TEXTAREA) add a multiline TextArea to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI(), OnGUI():
stringResult = GUILayout.TextArea(stringResult, 256, GUILayout.Height(96)); // 256 is max Characters

/* ----- (PASSWORD) add a masked Password field to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI(), OnGUI():
stringResult = GUILayout.PasswordField(stringResult, "*"[0], 32); // "*"[0] is char "*", 32 is max Characters

/* ----- (CHECKBOX) add a Checkbox to the Inspector ----- */
/// Class Body:
private bool toggleResult = false;

/// OnInspectorGUI(), OnGUI():
toggleResult = EditorGUILayout.Toggle("Test", toggleResult);

/* ----- (CHECKBOX) add a Checkbox Group to the Inspector ----- */
/// Class Body:
private bool toggleGroupResult = false;
private bool toggleResult1 = false;
private bool toggleResult2 = false;
private bool toggleResult3 = false;

/// OnInspectorGUI(), OnGUI():
toggleGroupResult = EditorGUILayout.BeginToggleGroup("Label Text", toggleGroupResult);
EditorGUILayout.Toggle("Test1", toggleResult1);
EditorGUILayout.Toggle("Test2", toggleResult2);
EditorGUILayout.Toggle("Test3", toggleResult3);
EditorGUILayout.EndToggleGroup();

/* ----- (RADIO BUTTONS) add a Radio Button Group to the Inspector ----- */
/// Class Body:
private int selectedIndex = 1;
string[] text = new string[] { "option1", "option2", "option3", "option4" };

/// OnInspectorGUI(), OnGUI():
selectedIndex = GUILayout.SelectionGrid(selectedIndex, text, 1, EditorStyles.radioButton); // 1 is how many controls to fit in one line

/* ----- (COLOR) add a Colorpicker to the Inspector ----- */
/// Class Body:
private Color myColor = Color.white;

/// OnInspectorGUI(), OnGUI():
myColor = EditorGUILayout.ColorField("Color", myColor);

/* ----- (SLIDER) add a Slider to the Inspector ----- */
/// Class Body:
private int sliderResultInt = 0;
private float sliderResultFloat = 0.0f;

/// OnInspectorGUI(), OnGUI():
sliderResultInt = EditorGUILayout.IntSlider("Test", sliderResultInt, 0, 10); // int slider
sliderResultFloat = EditorGUILayout.Slider("Test", sliderResultFloat, 0f, 10f); // float slider

/* ----- (SLIDER) add a MinMax Slider to the Inspector ----- */
/// Class Body:
private float sliderMin = 0;
private float sliderMax = 0;

/// OnInspectorGUI(), OnGUI():
EditorGUILayout.MinMaxSlider(ref sliderMin, ref sliderMax, 0, 100);

/* ----- (IMAGE) add an Image to the Inspector from a "Resources" folder, expanded 100% & resizable ----- */
/// OnInspectorGUI(), OnGUI():
Texture2D image = Resources.Load<Texture2D>("myImage");
if (image) {
	
	Rect rectangle;
	float imageHeight = image.height;
	float imageWidth  = image.width;
	float result      = imageHeight / imageWidth;
	float screenWidth = Screen.width;
	result = result * screenWidth;
	rectangle = GUILayoutUtility.GetRect(imageHeight, result);
	EditorGUI.DrawTextureTransparent(rectangle, image);
	
}

/* ----- (TOOLBAR) add a Toolbar with Buttons ----- */
/// Class Body:
private int myToolBarSelectedItem = 0;

/// OnInspectorGUI(), OnGUI():
Texture[] buttonImages = new Texture[3]; // will also create 3 Toolbar Buttons
buttonImages[0] = Resources.Load<Texture2D>("myIcon");
buttonImages[1] = Resources.Load<Texture2D>("myIcon");
buttonImages[2] = Resources.Load<Texture2D>("myIcon");

int myToolBar = GUILayout.Toolbar(myToolBarSelectedItem, buttonImages, GUILayout.ExpandWidth(true), GUILayout.MinHeight(32));

switch(myToolBar) {

	case 0:
		Debug.Log("Toolbar Button 1 is selected..."); 
		myToolBarSelectedItem = 0; // changes current selected item
		break;
	case 1:
		Debug.Log("Toolbar Button 2 is selected...");  
		myToolBarSelectedItem = 1;
		break;
	case 2:
		Debug.Log("Toolbar Button 3 is selected...");  
		myToolBarSelectedItem = 2;
		break;

}

/* ----- (DROPDOWN) add a custom value Dropdown menu ----- */
/// Class Body:
int selectedIndex = 0;
string[] choices = new string[]{ "One", "Two", "Three" };

/// OnInspectorGUI(), OnGUI():
selectedIndex = EditorGUILayout.Popup("Label Text", selectedIndex, choices, EditorStyles.popup);

/* ----- (DROPDOWN) add an Enum Dropdown menu ----- */
/// Class Body:
public enum MyChoices
{
	Choice1,
	Choice2,
	Choice3
}
public MyChoices selectedIndex;

/// OnInspectorGUI(), OnGUI():
selectedIndex = (MyChoices)EditorGUILayout.EnumPopup("Label Text", selectedIndex, EditorStyles.popup);

/* ----- (FOLDOUT) add a Foldout (Accordion) ----- */
/// Class Body:
private bool myFoldoutState = false;

/// OnInspectorGUI(), OnGUI():
myFoldoutState = EditorGUILayout.Foldout(myFoldoutState, "More");
if (myFoldoutState) {
	
	if( GUILayout.Button("Inside Button")) {}

}

/* ----- (HELPBOX) add a Help Box (Message) ----- */
/// OnInspectorGUI(), OnGUI():
EditorGUILayout.HelpBox("Some Warning message", MessageType.Warning);

/* ----- (CURVE) show an Animation Curve graph ----- */
/// Class Body:
private AnimationCurve myCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

/// OnInspectorGUI(), OnGUI():
myCurve = EditorGUILayout.CurveField("Curve", myCurve);

/* ----- (LAYER) show a Layer dropdown ----- */
/// Class Body:
private int myLayer = 0;

/// OnInspectorGUI(), OnGUI():
myLayer = EditorGUILayout.LayerField("Layer", myLayer);

/* ----- (DROPZONE) create an Asset Dropzone ----- */
Event thisEvent = Event.current;
Rect  dropZone  = GUILayoutUtility.GetRect(0.0f, 80.0f, GUILayout.ExpandWidth(true));
EditorGUILayout.Space();
GUI.Box(dropZone, "", GUI.skin.textField);
GUIStyle guiLabelCentered  = new GUIStyle("label");
guiLabelCentered.alignment = TextAnchor.MiddleCenter;
GUI.Label(dropZone, "Drop Assets Here", guiLabelCentered);
EditorGUILayout.Space();

switch (thisEvent.type) {

	case EventType.DragUpdated:
	case EventType.DragPerform:

	if (!dropZone.Contains(thisEvent.mousePosition)) {
		return;
	}
		
	DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
	
	if (thisEvent.type == EventType.DragPerform) {

		DragAndDrop.AcceptDrag();
		
		foreach (Object droppedObject in DragAndDrop.objectReferences) {

			Debug.Log("onDrop: Dropped Asset -> "+droppedObject.name);
			// handle dropped assets here ...
		}

	}

	break;
}

/* ----- (RECTANGLE) draw a rectangle ----- */
/// OnInspectorGUI(), OnGUI():
Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(EditorGUIUtility.singleLineHeight));
EditorGUI.DrawRect(new Rect(0, rect.y, Screen.width, rect.height), new Color32(0x42, 0x80, 0xe4, 0xff)); // use rect.x & rect.width instead to apply Inspector margins


/* --------------------------------------------
   Asset Preview
-------------------------------------------- */
// create an asset preview thumbnail
Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(EditorGUIUtility.singleLineHeight));

Texture2D icon = AssetPreview.GetAssetPreview(prefab); // supply prefab as a GameObject
Rect iconRect = new Rect(rect.x, rect.y, rect.height, rect.height);

if (icon != null) {
	GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit, true, 1f, Color.white, Vector4.zero, Vector4.one * 4f);
} else {
	EditorGUI.DrawRect(iconRect, EditorStyles.label.normal.textColor * 0.25f);
}

// repaint Editor Window if asset preview is still loading
if (AssetPreview.IsLoadingAssetPreviews()) {
	this.Repaint(); // "this" is EditorWindow
}


/* --------------------------------------------
   Manipulate Inspector Custom UI Controls
-------------------------------------------- */
// set the button below to be disabled
GUI.enabled = false; // disable the following:
if (GUILayout.Button("Test")) {
	// can't click this since it is disabled ...
}
GUI.enabled = true; // enable the next elements


/* -----------------------------------------
   Editor Window
----------------------------------------- */

// shows a CustomEditor in a Window instead of a Component/Inspector

// -------------( SampleWindowEditor.cs )--------------

using UnityEditor;
using UnityEngine;

public class SampleWindowEditor : EditorWindow
{
    [MenuItem("Tools/Open Custom Window...")]
    static void OpenCustomEditor()
    {

        SampleWindowEditor window = (SampleWindowEditor)GetWindow(typeof(SampleWindowEditor));
		
        window.position     = new Rect(6, 100, 320, 200);    // initial window position
		window.titleContent = new GUIContent("Test Window"); // window title

		// set window icon (optional)
		Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Gizmos/SampleWindowEditor Icon.png"); // load window icon
		window.titleContent = new GUIContent("Test Window", icon); // window title & icon

		// handle resizing, same sizes for min and max size will disable resizing of the window
		window.minSize = new Vector2(320, 200);
        window.maxSize = new Vector2(320, 200);
		
		window.Show();	// show the window
		window.Focus(); // keyboard focus the window

		// OR create a persistent popout window
		SampleWindowEditor window2 = ScriptableObject.CreateInstance(typeof(SampleWindowEditor)) as SampleWindowEditor;
        window2.ShowUtility(); // show popout 
		
		// OR create a non persistent popout window
		SampleWindowEditor window2 = ScriptableObject.CreateInstance(typeof(SampleWindowEditor)) as SampleWindowEditor;
        window2.ShowAuxWindow(); // show popout (autocloses when user clicks outside the window) 
		
    }

    void OnGUI()
    {

		// window contents
	
		if (GUILayout.Button("Close")) {
			this.Close(); // close the window
		}

	}

}

// -------------( SampleWindowEditor.cs )--------------


/* -----------------------------------------
   Scriptable Objects
----------------------------------------- */

// creates a scriptable object / custom asset

// -------------( SampleScriptableAsset.cs )--------------

/* note: if there's a "SampleScriptableAsset Icon" texture in "Assets/Gizmos" folder, this script will use it for the file icon */

using UnityEngine;

// accessible via Create > New Scriptable Asset
[CreateAssetMenu(fileName = "Assets/NewScriptableAsset", menuName = "New Scriptable Asset", order = int.MaxValue)]
public class PrefabSwatchAsset : ScriptableObject
{

	// asset contents

    [System.Serializable]
    public struct SomeClass {
        string name;
        [Range(0, 1)]
        float value;
    }

    [SerializeField]
    public SomeClass customClass;

    public GameObject[] somePrefabCollection = new GameObject[3];

    public string someText = "test";

    [Range(0, 1000)]
    public float someNumber = 3;

}

// -------------( SampleScriptableAsset.cs )--------------


/* -----------------------------------------
   Editor Preferences Storage
----------------------------------------- */
/* store a setting in the Editor Preferences Database */

// store an Int in a key named "testing"
EditorPrefs.SetInt("testing", 12); // also SetFloat, SetString, SetBool 

// retrieve an Int from the database named "testing"
int someOption = EditorPrefs.GetInt("testing"); // also GetFloat, GetString, GetBool 

// deletes the key from the database named "testing"
EditorPrefs.DeleteKey("testing");

// clears ALL Editor preference keys (use with caution!)
EditorPrefs.DeleteAll();


/* -----------------------------------------
   Editor Preferences Item
----------------------------------------- */

// adds a custom section & interface in the Unity Preferences section (Edit > Preferences)

// -------------( SamplePreferencesEditor.cs )--------------


using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class SamplePreferencesEditor
{
    
    // preference variables
    public static bool   optionVar1 = false;
    public static string optionVar2 = "";
    public static int    optionVar3 = 0;
    
    [SettingsProvider]
    public static SettingsProvider PreferenceItem()
    {

        // category and scope (where the settings tab will appear | Scope = User (Preferences), Scope = Project (Project Settings))
        var provider = new SettingsProvider("Preferences/My Preference Item", SettingsScope.User)
        {

            // item label
            label = "My Preference Item",
            
            // GUI items
            guiHandler = (searchContext) =>
            {

                // load stored preferences from EditorPrefs entries
                optionVar1 = EditorPrefs.GetBool("optionVar1", false); // "false" is default value
                optionVar2 = EditorPrefs.GetString("optionVar2", "");
                optionVar3 = EditorPrefs.GetInt("optionVar3", 0);

                // preferences user interface
                optionVar1 = EditorGUILayout.Toggle("Option 1", optionVar1);
                optionVar2 = EditorGUILayout.TextField("Option 2", optionVar2);
                optionVar3 = EditorGUILayout.IntSlider("Option 3", optionVar3, 0, 100);

                // save user changes
                if (GUI.changed) {
                    // save stored preferences to EditorPrefs entries
                    EditorPrefs.SetBool("optionVar1", optionVar1);
                    EditorPrefs.SetString("optionVar2", optionVar2);
                    EditorPrefs.SetInt("optionVar3", optionVar3);
                }

            },

            // search keywords
            keywords = new HashSet<string>(new[] { "custom", "options" })

        };

        return provider;
    }
}



// -------------( SamplePreferencesEditor.cs )--------------


/* -----------------------------------------
   Project Assets
----------------------------------------- */

// refresh & import unimported items to the Assets database
AssetDatabase.Refresh();

// saves all unsaved item changes to disk
AssetDatabase.SaveAssets();

// check if selected asset exists
Object testAsset = (Object)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject), typeof(Object));
if (testAsset !=null && AssetDatabase.Contains(testAsset)) {
	Debug.Log("Asset "+testAsset.name+" exists");
}

// safely delete selected asset (move to Trash/Recycle Bin)
Object testAsset = (Object)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject), typeof(Object));
if (testAsset !=null && AssetDatabase.Contains(testAsset)) {
	Debug.Log("Removing asset "+testAsset.name);
	AssetDatabase.MoveAssetToTrash( AssetDatabase.GetAssetPath( testAsset ) );
}

// retrieve assets of type by their GUID
foreach (string guid in AssetDatabase.FindAssets("t:Sprite")) {

	string path = AssetDatabase.GUIDToAssetPath(guid);
	Sprite spr = AssetDatabase.LoadAssetAtPath<Sprite>(path);
	if (spr != null) {
		Debug.Log(spr.name);
		// work with loaded assets, add to list etc ...
	}

}


/* -----------------------------------------
   Project Scene Assets
----------------------------------------- */

// retrieve the currently active scene in the Editor
Scene activeScene = SceneManager.GetActiveScene();

// save current Scene
EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

// loads a different initial scene everytime you test run the game, e.g. a "Setup.unity" scene under {ProjectFolder}/Assets/Scenes/
SceneAsset startupScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Setup.unity");
EditorSceneManager.playModeStartScene = startupScene;

// open a scene in the Editor and set it as active
Scene openScene = EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
EditorSceneManager.SetActiveScene(openScene);

// close a scene in the Editor, e.g. current scene
EditorSceneManager.CloseScene(EditorSceneManager.GetActiveScene(), true); // true is a flag to remove the scene after closing

// retrieve a scene by its name
Scene findScene = EditorSceneManager.GetSceneByName("Level1");


/* -----------------------------------------
   Components
----------------------------------------- */
// copy component values
ComponentUtility.CopyComponent(targetComponent); 

// paste component values
ComponentUtility.PasteComponentValues(targetComponent);

// duplicate component
System.Type type = original.GetType(); // original is Component
Component copy   = destination.AddComponent(type);  // destination is of type GameObject
EditorUtility.CopySerialized(original, copy);


/* -----------------------------------------
   Project Settings
----------------------------------------- */

// launch Build Settings
EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));

// launch Project Settings
// valid values: Player, Input, Tags and Layers, Physics, Physics 2D, Time, Quality, Audio, Editor, Script Execution Order, Preset Manager
EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player"); 


/* -----------------------------------------
   Player Settings (Editor)
----------------------------------------- */

// enable exlusive audio (Android & iOS)
// mutes all background audio when app is active
// note: requires READ_PHONE_STATE permission
PlayerSettings.muteOtherAudioSources = true;

// hide OS Statusbar (iOS Only)
PlayerSettings.statusBarHidden = true;

// disable Screen Rotation Animation (Android & iOS)
PlayerSettings.useAnimatedAutorotation = false;

// allow application in background
PlayerSettings.visibleInBackground = true;


/* -----------------------------------------
   Game Playback
----------------------------------------- */

// stop game playback
EditorApplication.isPlaying = false;

// test run the game
EditorApplication.isPlaying = true;


/* -----------------------------------------
   Change Active Editor Tool
----------------------------------------- */
Tools.current = Tool.Move;

/*
	Tools:
		None, View, Move, Rotate, Scale, Rect, Transform
*/


/* -----------------------------------------
   Reset Game View
----------------------------------------- */
MethodInfo info = SceneView.lastActiveSceneView.GetType().GetMethod("OnNewProjectLayoutWasCreated", BindingFlags.Instance | BindingFlags.NonPublic);
info.Invoke(SceneView.lastActiveSceneView, null);


/* -----------------------------------------
   File Manipulation
----------------------------------------- */
// highlight file or folder in Finder/Explorer
EditorUtility.RevealInFinder(Application.persistentDataPath + "/");


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


/* -----------------------------------------
   Quit Editor
----------------------------------------- */
// save & exit
EditorApplication.SaveScene(EditorApplication.currentScene);
EditorApplication.Exit(0);

// exit without saving
EditorApplication.Exit(0);


/* -----------------------------------------
   Editor Menu Items
----------------------------------------- */
// adds a menu item for the Editor
// hotkeys can be added with special modifier keys % (ctrl on Windows, cmd on macOS), # (shift), & (alt)

#if UNITY_EDITOR
public class EditorShortCutKeys : ScriptableObject {
	
	[MenuItem("Tools/CustomItem _F5")]
	static void EditorHotkeyTestPlay() {
		// MainMenu > Tools > CustomItem was pressed, or F5 key was pressed
		Debug.Log("Menu Item selected...");
	}
	
	/// more ...
}
#endif

// execute menu item
EditorApplication.ExecuteMenuItem("Tools/CustomItem");

// display menu item at position
EditorUtility.DisplayPopupMenu(new Rect(0, 32, 0, 0), "GameObject", null);


/* -----------------------------------------
   Navigate to URL
----------------------------------------- */
Application.OpenURL("https://google.com");


/* -----------------------------------------
   Access the System Clipboard
----------------------------------------- */
// copy to clipboard
EditorGUIUtility.systemCopyBuffer = "Test";

// paste from clipboard
string testString = EditorGUIUtility.systemCopyBuffer;

