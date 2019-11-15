
/**
 * Editor.AssetDatabase.cs
 * Editor Asset Database related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEditor.SceneManagement; // for EditorSceneManager
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
   Project Assets
----------------------------------------- */

// refresh & import unimported items to the Assets database
AssetDatabase.Refresh();

// saves all unsaved item changes to disk
AssetDatabase.SaveAssets();

// open an asset using the default action
AssetDatabase.OpenAsset(Selection.activeObject);

// open an asset in an external editor using the default action and go to specified line and column
AssetDatabase.OpenAsset(Selection.activeObject, 5, 2); // goes to line 5 and column 2

// validate selected asset as folder
Object testAsset = Selection.activeObject;
if (AssetDatabase.IsValidFolder((AssetDatabase.GetAssetPath(testAsset)))) {
	Debug.Log("Current selected asset is Folder");
}

// validate selected asset as file
Object testAsset = Selection.activeObject;
if ((AssetDatabase.Contains(testAsset)) && !AssetDatabase.IsValidFolder((AssetDatabase.GetAssetPath(testAsset)))) {
	Debug.Log("Current selected asset is File");	
}

// validate selected asset as Scene
Object testAsset = Selection.activeObject;
if (AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GetAssetPath(testAsset)) == typeof(SceneAsset)) {
	Debug.Log("Current selected asset is Scene");
}

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

// trigger a user save dialog, usually before changing scenes
EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();


/* -----------------------------------------
   Asset Highlight
----------------------------------------- */

// highlight object in Asset view or Hierarchy (e.g. current active gameObject)
EditorGUIUtility.PingObject(Selection.activeGameObject);

// highlight asset source file or folder in Finder/Explorer
EditorUtility.RevealInFinder(Application.persistentDataPath + "/");

