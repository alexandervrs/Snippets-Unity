
/**
 * Events.cs
 * Event related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.SceneManagement; // for SceneManager (Used for Scene Events) only


/* -----------------------------------------
   Initialization Events
----------------------------------------- */

void Awake()
{
    Debug.Log("On gameObject Awake");
}

void Start() 
{
    Debug.Log("On gameObject Start");
}

void OnEnable() 
{
    Debug.Log("On gameObject Enable");
}


/* -----------------------------------------
   Update Events
----------------------------------------- */

void Update()
{
    Debug.Log("On Update");
}

// FixedUpdate is often called more frequently than Update. It can be called multiple times per frame, if the frame rate is low and it may not be called between frames at all if the frame rate is high. All physics calculations and updates occur immediately after FixedUpdate. It should be used instead of Update when dealing with Rigidbody.
// note: you do not need to multiply your values by Time.deltaTime. This is because FixedUpdate is called on a reliable timer, independent of the frame rate
void FixedUpdate() 
{
    Debug.Log("On Fixed Update");
}

// LateUpdate is called once per frame, after Update has finished
void LateUpdate()
{
    Debug.Log("On Late Update");
}


/* -----------------------------------------
   Rendering Events
----------------------------------------- */

// called from Camera before rendering
void OnPreRender()
{
    Debug.Log("On PreRender");
}

// called from Camera for PostProcessing effects on the Camera RenderTexture
void OnRenderImage(RenderTexture source, RenderTexture destination)
{
    Debug.Log("On Render Image");
    Graphics.Blit(source, destination);
}

// called from any Object
void OnRenderObject()
{
    Debug.Log("On Render Object");
}

// called from Camera after regular rendering
void OnPostRender()
{
    Debug.Log("On PostRender");
}

// executes when the renderer is no longer visible by any camera
void OnBecameInvisible()
{
    Debug.Log("Out of View");
}

// executes when the renderer is visible by any camera
void OnBecameVisible()
{
    Debug.Log("Inside View");
}



/* -----------------------------------------
   Cleanup Events
----------------------------------------- */

void OnDisable()
{
    Debug.Log("On gameObject Disable");
}

void Destroy()
{
    Debug.Log("On gameObject Destroy");
}

void OnApplicationQuit()
{
    Debug.Log("Application ending after " + Time.time + " seconds");
}


/* -----------------------------------------
   Application Focus Events
----------------------------------------- */

// called when application loses and regains keyboard focus
void OnApplicationFocus(bool hasFocus)
{
    Debug.Log("Application has Focus? " +hasFocus);
}

// called when application is paused (e.g. when hardware Home button is pressed)
void OnApplicationPause(bool pauseStatus)
{
    Debug.Log("Application is Paused? " +pauseStatus);
}


/* -----------------------------------------
   Collision Events
----------------------------------------- */

/* note: Collision Events concern Rigidbodies and Colliders (see Physics.cs snippet file) */

void OnCollisionEnter(Collision collisionInfo)
{
    Debug.Log("On Collision Enter with "+collisionInfo.gameObject.name); 
}

void OnCollisionStay(Collision collisionInfo)
{
    Debug.Log("On Collision Stay with "+collisionInfo.gameObject.name);
}

void OnCollisionExit(Collision collisionInfo)
{
    Debug.Log("On Collision Exit with "+collisionInfo.gameObject.name);
}


/* -----------------------------------------
    Scene Events
----------------------------------------- */

/* OnSceneLoaded event */
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


/* -----------------------------------------
    Editor-only Events
----------------------------------------- */

// called when the GUI is being rendered (usually for EditorWindow)
void OnGUI()
{
	Debug.Log("OnGUI");
}

// called when the inspector GUI is being rendered (usually for Inspector)
void override OnInspectorGUI() {
	Debug.Log("OnInspectorGUI");	
}

// called when user selection on the Unity Editor has changed
void OnSelectionChange() {
	Debug.Log("OnSelectionChange");
}

// called from the Scene window, but needs to be registered first
// usually for mesh editing or displaying custom gizmos in the Scene window

/// OnEnable():
SceneView.duringSceneGui -= OnSceneGUI;
SceneView.duringSceneGui += OnSceneGUI;

/// Class Body:
void OnSceneGUI(SceneView view) {
	Debug.Log("OnSceneGUI: "+view);
}