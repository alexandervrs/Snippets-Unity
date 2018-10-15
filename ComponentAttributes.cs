
/**
 * ComponentAttributes.cs
 * Component Attribute related snippets for Unity
 */

/* using */
using UnityEngine;
using System; // for Serializable


/* -----------------------------------------
   Class Attributes
----------------------------------------- */
// note: add before class definition

// adds a required component of specified type
[RequireComponent(typeof(AudioSource))]

// disallows adding component of same type again
[DisallowMultipleComponent]

// also execute events in Edit Mode
[ExecuteInEditMode]

// define a Documentation URL
[HelpURL("http://example.com/docs/MyComponent.html")]

// initialize script on app load
[InitializeOnLoad]


/* -----------------------------------------
   Property Attributes
----------------------------------------- */
// note: add inside class body

// hides the following property from the Inspector
[HideInInspector]
public int hiddenProperty;

// shows the following PRIVATE property in the Inspector
[SerializeField]
private int unhiddenProperty;

// adds a header text
[Header("Title Text")]
public int someProperty;

// adds a tooltip description text
[Tooltip("Help text")]
public int someProperty2;

// adds spacing between last property
[Space(10)]
public int someProperty3;

// adds a UI slider (Float)
[Range(0.0f, 1.0f)]
public float someRangeFloat = 0.0f;

// adds a UI slider (Int)
[Range(0, 100)]
public int someRangeInt = 0;

// adds a UI checkbox
public bool someOptionToggle = 0.0f;

// adds a UI textfield
public string someText = "";

// adds a multiline textbox
[Multiline]
public string someTextArea = "";

// adds a combobox (based on an enum)
public SelectOptions someChoices;
public enum SelectOptions {
    Option1,
    Option2,
    Option3
}

// adds a UI Vector input
public Vector2 someCoords2;
public Vector3 someCoords3;
public Vector4 someCoords4;

// adds an string array
string[] sampleArray = new string[5];

// adds a Dictionary list
[Serializable]
public class MyDictionaryModel
{
    public string name;
    public float value;
}
[SerializeField]
private List<MyDictionaryModel> sampleDictionary;

