
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

// -------------( MyCustomEditor.cs )--------------

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
public class MyCustomEditor : Editor
{
	
	public override void OnInspectorGUI()
    {

		MyComponent targetComponent = (MyComponent)target;
		
		// draws the default Inspector items
		DrawDefaultInspector();

		if (GUILayout.Button("Test")) {
			//targetComponent.MyMethod(); // call a "MyMethod()" from targetComponent
			Debug.Log("Button Clicked...");
		}

	}

}

// -------------( MyCustomEditor.cs )--------------

// target the specific Component Inspector
MyComponent targetComponent = (MyComponent)target;

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

/// OnInspectorGUI():
stringResult = GUILayout.TextField(stringResult, 32); // 32 is max Characters

/* ----- (TEXTFIELD) add a Label & Textfield to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI():
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

/// OnInspectorGUI():
myNumberInt = EditorGUILayout.IntField("Label Text", myNumberInt);
myNumberFloat = EditorGUILayout.FloatField("Label Text", myNumberFloat);

/* ----- (TEXTAREA) add a multiline TextArea to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI():
stringResult = GUILayout.TextArea(stringResult, 256, GUILayout.Height(96)); // 256 is max Characters

/* ----- (PASSWORD) add a masked Password field to the Inspector ----- */
/// Class Body:
private string stringResult = "";

/// OnInspectorGUI():
stringResult = GUILayout.PasswordField(stringResult, "*"[0], 32); // "*"[0] is char "*", 32 is max Characters

/* ----- (CHECKBOX) add a Checkbox to the Inspector ----- */
/// Class Body:
private bool toggleResult = false;

/// OnInspectorGUI():
toggleResult = EditorGUILayout.Toggle("Test", toggleResult);

/* ----- (CHECKBOX) add a Checkbox Group to the Inspector ----- */
/// Class Body:
private bool toggleGroupResult = false;
private bool toggleResult1 = false;
private bool toggleResult2 = false;
private bool toggleResult3 = false;

/// OnInspectorGUI():
toggleGroupResult = EditorGUILayout.BeginToggleGroup("Label Text", toggleGroupResult);
EditorGUILayout.Toggle("Test1", toggleResult1);
EditorGUILayout.Toggle("Test2", toggleResult2);
EditorGUILayout.Toggle("Test3", toggleResult3);
EditorGUILayout.EndToggleGroup();

/* ----- (RADIO BUTTONS) add a Radio Button Group to the Inspector ----- */
/// Class Body:
private int selectedIndex = 1;
string[] text = new string[] { "option1", "option2", "option3", "option4" };

/// OnInspectorGUI():
selectedIndex = GUILayout.SelectionGrid(selectedIndex, text, 1, EditorStyles.radioButton); // 1 is how many controls to fit in one line

/* ----- (COLOR) add a Colorpicker to the Inspector ----- */
/// Class Body:
private Color myColor = Color.white;

/// OnInspectorGUI():
myColor = EditorGUILayout.ColorField("Color", myColor);

/* ----- (SLIDER) add a Slider to the Inspector ----- */
/// Class Body:
private int sliderResultInt = 0;
private float sliderResultFloat = 0.0f;

/// OnInspectorGUI():
sliderResultInt = EditorGUILayout.IntSlider("Test", sliderResultInt, 0, 10); // int slider
sliderResultFloat = EditorGUILayout.Slider("Test", sliderResultFloat, 0f, 10f); // float slider

/* ----- (SLIDER) add a MinMax Slider to the Inspector ----- */
/// Class Body:
private float sliderMin = 0;
private float sliderMax = 0;

/// OnInspectorGUI():
EditorGUILayout.MinMaxSlider(ref sliderMin, ref sliderMax, 0, 100);

/* ----- (IMAGE) add an Image to the Inspector from a "Resources" folder, expanded 100% & resizable ----- */
/// OnInspectorGUI():
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

/// OnInspectorGUI():
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

/// OnInspectorGUI():
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

/// OnInspectorGUI():
selectedIndex = (MyChoices)EditorGUILayout.EnumPopup("Label Text", selectedIndex, EditorStyles.popup);

/* ----- (FOLDOUT) add a Foldout (Accordion) ----- */
/// Class Body:
private bool myFoldoutState = false;

/// OnInspectorGUI():
myFoldoutState = EditorGUILayout.Foldout(myFoldoutState, "More");
if (myFoldoutState) {
	
	if( GUILayout.Button("Inside Button")) {}

}

/* ----- (HELPBOX) add a Help Box (Message) ----- */
/// OnInspectorGUI():
EditorGUILayout.HelpBox("Some Warning message", MessageType.Warning);

/* ----- (CURVE) show an Animation Curve graph ----- */
/// Class Body:
private AnimationCurve myCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

/// OnInspectorGUI():
myCurve = EditorGUILayout.CurveField("Curve", myCurve);

/* ----- (LAYER) show a Layer dropdown ----- */
/// Class Body:
private int myLayer = 0;

/// OnInspectorGUI():
myLayer = EditorGUILayout.LayerField("Layer", myLayer);


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
   Editor Preferences
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
   Project Assets
----------------------------------------- */

// refresh & import unimported items to the Assets database
AssetDatabase.Refresh();

// saves all unsaved item changes to disk
AssetDatabase.SaveAssets();

// save current Scene
EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());


/* -----------------------------------------
   Project Settings
----------------------------------------- */

// launch Build Settings
EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));

// launch Project Settings
// valid values: Player, Input, Tags and Layers, Physics, Physics 2D, Time, Quality, Audio, Editor, Script Execution Order, Preset Manager
EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player"); 


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

