
/**
 * Scene.cs
 * Scene Management related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.SceneManagement;


/* -----------------------------------------
   Load Scene
----------------------------------------- */
// load scene sync
SceneManager.LoadScene("TestScene", LoadSceneMode.Single);

// load scene sync, overlapping
SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);


/* -----------------------------------------
   Load Scene Async
----------------------------------------- */
// start loading
StartCoroutine(LoadSceneAsync());

// use ienumerator to load and check status
IEnumerator LoadSceneAsync()
{
	AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TestScene", LoadSceneMode.Single);
	
	if (asyncLoad == null) { 
		yield break;
	}
	
	while (!asyncLoad.isDone)
	{
		// query asyncLoad.progress here for progress (0.0f to 1.0f)
		yield return null;
	}
	
	// loading done ...
}

/* -----------------------------------------
   Restart Scene
----------------------------------------- */
// restart current scene
SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);


/* -----------------------------------------
   Move between scenes
----------------------------------------- */
// move to next scene
int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
if (SceneManager.sceneCount > nextSceneIndex) {
	SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
}

// move to previous scene
int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
if (previousSceneIndex >= 0) {
	SceneManager.LoadScene(previousSceneIndex, LoadSceneMode.Single);
}


/* -----------------------------------------
   Unload Scene Async
----------------------------------------- */
// note: 1. SceneManager.UnloadScene() is not recommended by Unity
//       2. SceneManager.UnloadSceneAsync() currently doesn't unload assets, use Resources.UnloadUnusedAssets() to free memory

// start unloading
StartCoroutine(UnLoadSceneAsync());

// use ienumerator to unload and check status
IEnumerator UnLoadSceneAsync()
{
	AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("TestScene");

	if (asyncUnload == null) { 
		yield break;
	}

	while (!asyncUnload.isDone)
	{
		// query asyncUnload.progress here for progress (0.0f to 1.0f)
		yield return null;
	}
	
	// unloading scene done ...

	AsyncOperation asyncUnload2 = Resources.UnloadUnusedAssets();

	if (asyncUnload2 == null) { 
		yield break;
	}

	while (!asyncUnload2.isDone)
	{
		// query asyncUnload2.progress here for progress (0.0f to 1.0f)
		yield return null;
	}

	// unloading unused assets done ...

	yield return null;
	
}

/* -----------------------------------------
   On Scene Loaded Event
----------------------------------------- */
// checks if Scene is loaded and ready

// first add OnSceneLoaded delegate
void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

// also remove OnSceneLoaded delegate on Scene/Game end
void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

// scene loaded event
void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    Debug.Log("Scene Loaded: " + scene.name);
	// scene is loaded, do stuff ...
}

