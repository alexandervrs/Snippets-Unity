
/**
 * Model.cs
 * 3D Model related snippets for Unity
 */

/* using */
using UnityEngine;
using System.Collections;


/* -----------------------------------------
   Mesh Operations
----------------------------------------- */
// set model visible or hidden
gameObject.GetComponent<MeshRenderer>().enabled = false;


/* -----------------------------------------
   Flip Mesh Normals
----------------------------------------- */

/*

    note: This is required in order to display a texture on a model on the inside and not outside
          e.g. a skybox, sphere or cylinder

*/

// -------------( MeshFlipNormals.cs )--------------
 
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshFlipNormals : MonoBehaviour {
 
	void Awake() {
		
		MeshFilter filter = GetComponent<MeshFilter>();
		
		if (filter != null) {
			
			Mesh mesh = filter.mesh;
 
			Vector3[] normals = mesh.normals;
			
			for (int i = 0; i < normals.Length; i++) {
				normals[i] = -normals[i];
			}
			
			mesh.normals = normals;
 
			for (int m = 0; m < mesh.subMeshCount; m++) {
				
				int[] triangles = mesh.GetTriangles(m);
				
				for (int i = 0; i < triangles.Length; i+=3) {
					
					int temp = triangles[i + 0];
					triangles[i + 0] = triangles[i + 1];
					triangles[i + 1] = temp;
					
				}
				
				mesh.SetTriangles(triangles, m);
			}
			
		}		
	}
}

// -------------( MeshFlipNormals.cs )--------------


/* -----------------------------------------
   Change Mesh Sorting Layer
----------------------------------------- */
/*

	note: Unity does not have Sorting Layer & Order exposed in the editor for MeshRenderer, 
          as a result this makes harder to mix 2D and 3D in some cases (e.g. TextMeshPro text) from within the Editor
          Save the below script inside an "Editor" folder and it will add fields for 2D Sort layer & order

	
*/

// -------------( MeshSortingLayer.cs )--------------  

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MeshRenderer))]
public class MeshSortingLayer : Editor
{

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();

        SerializedProperty sortingLayerID = serializedObject.FindProperty("m_SortingLayerID");
        SerializedProperty sortingOrder   = serializedObject.FindProperty("m_SortingOrder");

        EditorGUILayout.BeginVertical();

        string[] layerNames = GetSortingLayerNames();
        int[]    layerID    = GetSortingLayerUniqueIDs();

        int selected = -1;
        int sID = sortingLayerID.intValue;

        for (int i = 0; i < layerID.Length; i++) {

            if (sID == layerID[i]) {
                selected = i;
            }

        }

        if (selected == -1) {

            for (int i = 0; i < layerID.Length; i++) {

                if (layerID[i] == 0) {
                    selected = i;
                }

            }

        }

        selected                = EditorGUILayout.Popup("Sorting Layer", selected, layerNames);
        sortingLayerID.intValue = layerID[selected];
		
        EditorGUILayout.PropertyField(sortingOrder, new GUIContent("Order in Layer"));

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();

    }

    public string[] GetSortingLayerNames() {
		
        Type internalEditorUtilityType     = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
		
    }

    public int[] GetSortingLayerUniqueIDs() {
		
        Type internalEditorUtilityType             = typeof(InternalEditorUtility);
        PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
        return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
		
    }

}

// -------------( MeshSortingLayer.cs )--------------  

