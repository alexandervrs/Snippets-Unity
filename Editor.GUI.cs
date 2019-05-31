
/**
 * Editor.GUI.cs
 * Editor GUI related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Window / Inspector Custom UI Controls
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


/* ----- (IMAGE) add an Image to the Inspector from a "Resources" folder, stretch 100% across view area width and height ----- */
/// OnInspectorGUI(), OnGUI():
Texture2D image = Resources.Load<Texture2D>("myImage");
if (image) {

	Rect rectangle;
	rectangle = GUILayoutUtility.GetRect(Screen.width, EditorGUILayout.GetControlRect(GUILayout.Height(0)).height);
	EditorGUI.DrawTextureTransparent(componentRect, image);

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


/* ----- (SORTING LAYER) show a Sorting Layer dropdown ----- */
/// Class Body:
int defaultSortingLayerID;
int defaultSortingOrder = 0;
int defaultSortingLayerIDSelected;

string[] layerNames;
int[]    layerIDs;

/// OnEnable():
layerNames = GetSortingLayerNames();
layerIDs   = GetSortingLayerUniqueIDs();
defaultSortingLayerID = SortingLayer.GetLayerValueFromName("Default");

/// OnInspectorGUI(), OnGUI():
EditorGUILayout.BeginVertical();

	if (!SortingLayer.IsValid(defaultSortingLayerID)) {
		defaultSortingLayerID = SortingLayer.GetLayerValueFromName("Default");
		defaultSortingLayerIDSelected = 0;
	}

	defaultSortingLayerIDSelected = EditorGUILayout.Popup("Sorting Layer", defaultSortingLayerIDSelected, layerNames);
	defaultSortingLayerID = layerIDs[defaultSortingLayerIDSelected];

	defaultSortingOrder = EditorGUILayout.IntField("Order in Layer", defaultSortingOrder);
	
EditorGUILayout.EndVertical();


/* ----- (TAG) show a Tag dropdown ----- */
/// Class Body:
private string myTag = "Untagged";

/// OnInspectorGUI(), OnGUI():
myTag = EditorGUILayout.TagField("Tag", myTag);


/* ----- (FLAGS) show an enum collection as flags ----- */
/// Class Body:
public enum TestFlags {
	None = 0, // custom name for "Nothing" option
	A = 1 << 0,
	B = 1 << 1,
	AB = A | B, // combination of two flags
	C = 1 << 2,
	All = ~0, // custom name for "Everything" option
}
public TestFlags myenum = TestFlags.MyFlag1;

/// OnInspectorGUI(), OnGUI():
myenum = (TestFlags)EditorGUILayout.EnumFlagsField("Flags", myenum);


/* ----- (OBJECT) show an Object field ----- */
/// Class Body:
private Sprite myObject = null; // can be any Object type (Sprite, AudioClip, GameObject etc.)

/// OnInspectorGUI(), OnGUI():
myObject = EditorGUILayout.ObjectField(selectedObject, typeof(Sprite), true) as Sprite; // can be any Object type (Sprite, AudioClip, GameObject etc.)


/* ----- (SCROLLVIEW) add a Scrollview layout ----- */
/// Class Body:
private Vector2 scrollView = Vector2.zero;

/// OnInspectorGUI(), OnGUI():
EditorGUILayout.BeginScrollView(scrollView);

/// add more GUI elements here ...

EditorGUILayout.EndScrollView();


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


/* ----- (RECTANGLE) draw a rectangle as wide as the entire Component ----- */
/// OnInspectorGUI(), OnGUI():
Rect componentRect = EditorGUILayout.BeginVertical();
componentRect = new Rect(componentRect.x - 13, componentRect.y+3, EditorGUIUtility.currentViewWidth, componentRect.height + 18);
Color rectColor = new Color(0.4f, 0.4f, 0f, 0.3f);
EditorGUI.DrawRect(componentRect, rectColor);


/* --------------------------------------------
   Enable / Disable UI Controls
-------------------------------------------- */
// set the button below to be disabled
GUI.enabled = false; // disable the following:
if (GUILayout.Button("Test")) {
	// can't click this since it is disabled ...
}
GUI.enabled = true; // enable the next elements


/* --------------------------------------------
   Colorize UI Controls
-------------------------------------------- */

// set the color of the following controls
Color defaultBGColor = GUI.backgroundColor; // keep current BG color
Color defaultColor   = defaultColor; // keep current color

GUI.backgroundColor = Color.cyan; // BG color
GUI.color = Color.white; // text color
if (GUILayout.Button("Test")) {
	// ...
}
GUI.backgroundColor = defaultBGColor; // restore BG color
GUI.color = defaultColor; // restore color


// get a color based on theme
Color skinColor = EditorGUIUtility.isProSkin ? (Color)new Color32(56, 56, 56, 255) : (Color)new Color32(194, 194, 194, 255);


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
