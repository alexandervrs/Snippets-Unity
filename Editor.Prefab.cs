
/**
 * Editor.Prefab.cs
 * Editor Prefab Utility related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEditor.Experimental.SceneManagement; // for PrefabStage, PrefabStageUtility
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
   Create Prefab
----------------------------------------- */
// create prefab from GameObject, but don't connect to it
bool success = false;
PrefabUtility.SaveAsPrefabAsset(Selection.activeGameObject, "Assets/"+Selection.activeGameObject.name+".prefab", out success);
Debug.Log("Created Prefab: "+success);

// create prefab from GameObject, and connect to it
PrefabUtility.SaveAsPrefabAssetAndConnect(Selection.activeGameObject, "Assets/"+Selection.activeGameObject.name+".prefab", InteractionMode.AutomatedAction);


/* -----------------------------------------
   Unpack Prefab
----------------------------------------- */
// unpack/disconnect selected gameobject in the hierarchy (or can be any GameObject), and also any child prefabs
PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction); // Use InteractionMode.UserAction for Undo to be enabled

// unpack/disconnect only the outermost selected gameobject in the hierarchy (or can be any GameObject)
PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction); // Use InteractionMode.UserAction for Undo to be enabled


/* -----------------------------------------
   Manage Prefab Changes
----------------------------------------- */
// revert all prefab changes
PrefabUtility.RevertPrefabInstance(Selection.activeGameObject, InteractionMode.AutomatedAction);

// apply all prefab changes
PrefabUtility.ApplyPrefabInstance(Selection.activeGameObject, InteractionMode.AutomatedAction);

// mark as modified, prefab is saved after being marked as Dirty either manually or Auto in the Prefab Stage
PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
if (prefabStage != null) {
	UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(prefabStage.scene);
}


/* -----------------------------------------
   Prefab Info
----------------------------------------- */
// returns the corresponding gameobject of the prefab or null, can be used to repack a gameobject in a prefab
GameObject obj = PrefabUtility.GetCorrespondingObjectFromSource(myGameObjectOrComponent);

// get the outermost prefab instance handle, can be used to compare 2 GameObjects or Components and return if they are part of the same Prefab instance
GameObject obj = PrefabUtility.GetPrefabInstanceHandle(myGameObjectOrComponent);

