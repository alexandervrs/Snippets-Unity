
/**
 * ScriptableObject.cs
 * ScriptableObject related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Scriptable Objects
----------------------------------------- */

// creates a scriptable object / custom asset

// -------------( SampleScriptableAsset.cs )--------------

/* note: if there's a "SampleScriptableAsset Icon" texture in "Assets/Gizmos" folder, this script will use it for the file icon */

using UnityEngine;

// accessible via Create > New Scriptable Asset
[CreateAssetMenu(fileName = "Assets/NewScriptableAsset", menuName = "New Scriptable Asset", order = int.MaxValue)] // order is optional
public class SampleScriptableAsset : ScriptableObject
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
