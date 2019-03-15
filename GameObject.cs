
/**
 * GameObject.cs
 * GameObject related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.SceneManagement; // only for gameObject.scene
using System; // only for StringComparison


/* -----------------------------------------
   Create GameObject from Prefab
----------------------------------------- */
GameObject prefab; // supply a prefab

GameObject newObj = GameObject.Instantiate(prefab, new Vector3(
	gameObject.transform.position.x, 
	gameObject.transform.position.y, 
	gameObject.transform.position.z
	), Quaternion.identity
) as GameObject;

// (optional) set the sprite's draw order (if it has a SpriteRenderer component)
SpriteRenderer sprite   = newObj.GetComponent<SpriteRenderer>();
sprite.sortingLayerID   = SortingLayer.NameToID("Enemies"); // sorting layer name
sprite.sortingOrder     = 0;                                // order in that sorting layer


/* -----------------------------------------
   Create Empty GameObject
----------------------------------------- */
GameObject newObj = new GameObject();
newObj.name = "Test Object";

// parent the new empty gameobject to current gameObject's transform
newObj.transform.SetParent(gameObject.transform, true);


/* -----------------------------------------
   Find GameObject
----------------------------------------- */
/* get handle of current GameObject that is executing the script */
GameObject obj = gameObject;

/* find first Child GameObject by name in the current GameObject's transform/hierarchy */
GameObject obj = gameObject.transform.Find("Test").gameObject;

/* find first Child GameObject by index in the current GameObject's transform/hierarchy */
GameObject obj = gameObject.transform.GetChild(0).gameObject;

/* find all Child GameObjects under the current GameObject's transform/hierarchy */
for (int i = 0; i < gameObject.transform.childCount - 1; i++) {
	Debug.Log(gameObject.transform.GetChild(i).transform.name);
}

/* find any first GameObject by name */
GameObject obj = GameObject.Find("Test");

/* find all GameObjects by name (slow) */
GameObject[] objectsOfType = GameObject.FindObjectsOfType<GameObject>();
foreach (GameObject thisGameObject in objectsOfType) {
            
	if (thisGameObject.scene.isLoaded && thisGameObject.activeInHierarchy && thisGameObject.name == "Test") {

		Debug.Log(thisGameObject.name);
		// do stuff with thisGameObject ...

	}
}

/* find all GameObjects by Type */
dynamic[] objectList = FindObjectsOfType<Light>();
// check objectList.Length for number of Light objects found

/* find any GameObject with Tag */
// note: Manage Tags: Edit > Project Settings > Tags and Layers
GameObject obj = GameObject.FindWithTag("Enemies");

/* find all GameObjects with Tag */
GameObject[] taggedObj = GameObject.FindGameObjectsWithTag("Enemies");
foreach (GameObject obj in taggedObj) {
	GameObject.Instantiate(prefab, obj.transform.position, obj.transform.rotation);
}

/* find all GameObjects in Scene */
// define a method in Class Body
private List<GameObject> GetAllActiveObjectsInScene() {

	List<GameObject> objectsInScene = new List<GameObject>();
	GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

	foreach (GameObject thisGameObject in allObjects) {

		if (thisGameObject.scene.isLoaded && thisGameObject.activeInHierarchy) {

			Debug.Log(thisGameObject.name);
			objectsInScene.Add(thisGameObject);

		}

	}

	return objectsInScene;

}

// retrieve all GameObjects in a list by calling the above method
List<GameObject> gameobjectList = GetAllActiveObjectsInScene();

foreach (GameObject thisGameObject in gameobjectList) {
	Debug.Log(thisGameObject.name);
}


/* find all GameObjects in Scene (even Inactive ones) (Editor Only) */
#ifdef UNITY_EDITOR

// define a method in Class Body
private List<GameObject> GetAllObjectsInScene()
{
	List<GameObject> objectsInScene = new List<GameObject>();

	foreach (GameObject thisGameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
		
		if (thisGameObject.hideFlags == HideFlags.NotEditable || thisGameObject.hideFlags == HideFlags.HideAndDontSave) {
			continue;
		}
		
		if (!UnityEditor.EditorUtility.IsPersistent(thisGameObject.transform.root.gameObject)) {
			continue;
		}

		objectsInScene.Add(thisGameObject);
	}

	return objectsInScene;
}

// retrieve all GameObjects in a list by calling the above method
List<GameObject> gameobjectList = GetAllObjectsInScene();

foreach (GameObject thisGameObject in gameobjectList) {
	Debug.Log(thisGameObject.name);
}

#endif


/* -----------------------------------------
   Remove GameObject
----------------------------------------- */
// get handle of current GameObject
GameObject obj = gameObject;

// check if exists
if (GameObject.Find(obj)) {
	// destroy object
	GameObject.Destroy(obj);
}

// destroy after 3 seconds
GameObject.Destroy(gameObject, 3.0f);


/* -----------------------------------------
   Keep GameObject across Scenes
----------------------------------------- */
// flag the object not to be destroyed on Scene load
GameObject.DontDestroyOnLoad(gameObject);


/* -----------------------------------------
   Handle GameObject Properties
----------------------------------------- */
// get the GameObject's name
string objName = gameObject.name;

// get the GameObject's name (without the (Clone) part)
string objName = gameObject.name;
int clonePartIndex = gameObject.name.IndexOf(" (Clone)", StringComparison.Ordinal);
if (clonePartIndex != -1) {
    objName = gameObject.name.Substring(0, clonePartIndex);
}

// get the GameObject's transform (position, rotation, scale etc.)
Transform transform = gameObject.transform;

// get the GameObject's position (x,y,z) in World Space
Vector3 position = gameObject.transform.position;

// get the GameObject's rotation (rotationX,rotationY,rotationZ) in World Space
Quaternion rotation = gameObject.transform.rotation;

// get the GameObject's position (x,y,z), relative to the parent GameObject transform/hierarchy
Vector3 localPosition = gameObject.transform.localPosition;

// get the GameObject's scale (scaleX,scaleY,scaleZ), relative to the parent GameObject transform/hierarchy
Vector3 localScale = gameObject.transform.localScale;

// get the GameObject's position (rotationX,rotationY,rotationZ), relative to the parent GameObject transform/hierarchy
Quaternion localRotation = gameObject.transform.localRotation;

// get the GameObject's rotation (rotationX,rotationY,rotationZ) in degrees, relative to the parent GameObject transform/hierarchy
Vector3 rotationEulerAngles = gameObject.transform.localEulerAngles;

// get the GameObject's tag
string objTag = gameObject.tag;

// get the GameObject's layer
int objLayer = gameObject.layer;
Debug.Log( "GameObject Layer is: "+LayerMask.LayerToName(objLayer) );

// set the GameObject's layer by name
gameObject.layer = LayerMask.NameToLayer("Water");

// return the Scene the GameObject belongs to
Scene objScene = gameObject.scene;

// get the GameObject's children GameObjects
int children = gameObject.transform.childCount;

// get the GameObject's parent transform
Transform parent = gameObject.transform.parent;

// set GameObject to be active or inactive in scene, execute events, be accessible from other objects etc.
gameObject.SetActive(true);

// get if GameObject is active in a loaded scene
bool isActive = (gameObject.scene.isLoaded && gameObject.activeInHierarchy);

// change the parent object transform and make gameObject into a child in the Hierarchy under "someOtherGameObject"
gameObject.transform.SetParent(someOtherGameObject.transform, true); // "true" is WorldPositionStays, keeps world position, rotation, scale instead


/* -----------------------------------------
   Handle Components
----------------------------------------- */
// add a component
SphereCollider component = gameObject.AddComponent<SphereCollider>() as SphereCollider;

// get an existing component handle
SphereCollider component = gameObject.GetComponent<SphereCollider>();

// remove a component
GameObject.Destroy(gameObject.GetComponent<SphereCollider>());

// check if a component exists
SphereCollider component = gameObject.AddComponent<SphereCollider>() as SphereCollider;
if (component == null) {
	// Component doesn't exist on this GameObject ...
}

