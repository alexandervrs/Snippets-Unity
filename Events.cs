
/**
 * Events.cs
 * Event related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.SceneManagement; // for SceneManager (Used for Scene Events) only
using UnityEngine.Events; // for UnityEvent


/* -----------------------------------------
   Unity Events
----------------------------------------- */

// note: having a UnityEvent as public will show it as an Event List in the Inspector

/// Class Body:
public UnityEvent onCondition;

/// MyMethod():
// call all the Unity Events under "onCondition"
this.onCondition?.Invoke();


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
void OnRenderImage(RenderTexture sourceTexture, RenderTexture destinationTexture)
{
    Debug.Log("On Render Image");
    Graphics.Blit(sourceTexture, destinationTexture);
}

// called before rendering this Object
void OnWillRenderObject() {
    Debug.Log("On Will Render Object");	
}

// called from this Object
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
   Active State Events
----------------------------------------- */

void OnDisable()
{
    Debug.Log("On gameObject Disable");
}

void OnEnable() 
{
    Debug.Log("On gameObject Enable");
}


/* -----------------------------------------
   Destruct Events
----------------------------------------- */

// called when the gameObject is removed from the scene
void OnDestroy()
{
    Debug.Log("On gameObject Destroy");
}


/* -----------------------------------------
   Application Events
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

// called when application closes
void OnApplicationQuit()
{
    Debug.Log("Application ending after " + Time.time + " seconds");
}



/* -----------------------------------------
   Collision Events (Physics 3D)
----------------------------------------- */

/* note: Collision/Trigger Events concern Rigidbodies and Colliders (see Physics.cs snippet file) */

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

void OnTriggerEnter(Collider collisionInfo)
{
    Debug.Log("On Trigger Enter with "+collisionInfo.gameObject.name); 
}

void OnTriggerStay(Collider collisionInfo)
{
    Debug.Log("On Trigger Stay with "+collisionInfo.gameObject.name);
}

void OnTriggerExit(Collider collisionInfo)
{
    Debug.Log("On Trigger Exit with "+collisionInfo.gameObject.name);
}


/* -----------------------------------------
   Collision Events (Physics 2D)
----------------------------------------- */

/* note: Collision2D/Trigger2D Events concern Rigidbodies2D and Colliders2D (see Physics.cs snippet file) */

void OnCollisionEnter2D(Collision2D collisionInfo)
{
    Debug.Log("On Collision Enter with "+collisionInfo.gameObject.name); 
}

void OnCollisionStay2D(Collision2D collisionInfo)
{
    Debug.Log("On Collision Stay with "+collisionInfo.gameObject.name);
}

void OnCollisionExit2D(Collision2D collisionInfo)
{
    Debug.Log("On Collision Exit with "+collisionInfo.gameObject.name);
}

void OnTriggerEnter2D(Collider2D collisionInfo)
{
    Debug.Log("On Trigger Enter with "+collisionInfo.gameObject.name); 
}

void OnTriggerStay2D(Collider2D collisionInfo)
{
    Debug.Log("On Trigger Stay with "+collisionInfo.gameObject.name);
}

void OnTriggerExit2D(Collider2D collisionInfo)
{
    Debug.Log("On Trigger Exit with "+collisionInfo.gameObject.name);
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
void override OnInspectorGUI() 
{
	Debug.Log("OnInspectorGUI");	
}

// called when user selection on the Unity Editor has changed
void OnSelectionChange() 
{
	Debug.Log("OnSelectionChange");
}

// called from the Scene window, but needs to be registered first
// usually for mesh editing or displaying custom gizmos in the Scene window

/// OnEnable():
SceneView.duringSceneGui -= OnSceneGUI;
SceneView.duringSceneGui += OnSceneGUI;

/// Class Body:
void OnSceneGUI(SceneView view) 
{
	Debug.Log("OnSceneGUI: "+view);
}
