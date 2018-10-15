
/**
 * GameObject.cs
 * GameObject related snippets for Unity
 */

/* using */
using UnityEngine;


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
// get handle of current GameObject
GameObject obj = gameObject;

// find first GameObject by name
GameObject obj = GameObject.Find("Test");

// find all GameObjects by name (slow)
GameObject[] objectsOfType = GameObject.FindObjectsOfType<GameObject>();
foreach (GameObject thisGameObject in objectsOfType) {
            
	if (thisGameObject.name == "Test") {

		// do stuff with thisGameObject ...

	}
}

// find all GameObjects by Type
dynamic[] objectList = FindObjectsOfType<Light>();
// check objectList.Length for number of Light objects found
 

/* Manage Tags: Edit > Project Settings > Tags and Layers */

// find any GameObject with tag
GameObject obj = GameObject.FindWithTag("Enemies");

// find all GameObjects with tag
GameObject[] taggedObj = GameObject.FindGameObjectsWithTag("Enemies");
foreach (GameObject obj in taggedObj) {
	GameObject.Instantiate(prefab, obj.transform.position, obj.transform.rotation);
}


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
   Handle Components
----------------------------------------- */
// add a component
SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;

// get an existing component handle
SphereCollider sc = gameObject.GetComponent<SphereCollider>();

// remove a component
GameObject.Destroy(gameObject.GetComponent<SphereCollider>());

