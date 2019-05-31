
/**
 * Editor.Preferences.cs
 * Editor Preferences related snippets for Unity
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
   Launch Project Settings
----------------------------------------- */

// valid values: Player, Input, Tags and Layers, Physics, Physics 2D, Time, Quality, Audio, Editor, Script Execution Order, Preset Manager
EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player"); 

