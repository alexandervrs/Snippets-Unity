
/**
 * AssetBundle.cs
 * AssetBundle related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.U2D; // for SpriteAtlas
using UnityEngine.Networking; // for UnityWebRequest
using System.IO; // for Path


/* -----------------------------------------
   Setup Asset Bundle
----------------------------------------- */

/*

	1. Choose AssetBundle name at the bottom of the Inspector for each asset
	2. Compile AssetBundle for the target Platform using AssetBundle Browser or Editor script
	3. Load assets from bundle and use them
	
	notes: 
		1. Different platforms need a different AssetBundle type compiled from AssetBundle Browser
		2. WebGL needs a WebRequest in order to work (see section "Load AssetBundle (WebGL)")
		3. AssetBundles cannot contain Scripts but they can contain Prefabs/GameObjects with 
		   script component references as long as the Script used by the component exists in the main application
		4. If you assign a Scene to an Asset Bundle then it will reference all the assets the Scene is using automatically
           however you won't be able to add any additional assets in that bundle (e.g. unreferenced Shaders etc.) and you'll have to
           load them separately using an additional Asset Bundle	
		5. See Editor.BuildPipeline.cs on how to automate creating Asset Bundles	   
	
*/


/* -----------------------------------------
   Load files from AssetBundle
----------------------------------------- */ 
// load assets from AssetBundle asynchronously, this will not block the main thread and you can report back the progress to the user

/// IEnumerator Start(), IEnumerator MyMethod():
IEnumerator Start()
{

	/* case: extract and load entire Scene with all its assets */

	Debug.Log("Loading Asset Bundle...");

	// load scene from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "myscene"
	AssetBundleCreateRequest scenePackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/myscene"));

	yield return new WaitWhile(() => scenePackResult.isDone == false);

	Debug.Log("Asset Bundle Loaded");

	AssetBundle assetPack = scenePackResult.assetBundle as AssetBundle;
	string[] scenePaths   = assetPack.GetAllScenePaths();

	// load the first scene in the Asset Bundle, usually what you want
	AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Path.GetFileNameWithoutExtension(scenePaths[0]), LoadSceneMode.Single);

	if (asyncLoad == null) { 
		yield break;
	}
	
	while (!asyncLoad.isDone)
	{
		Debug.Log($"Loading Scene... {Path.GetFileNameWithoutExtension(scenePaths[0])}");
		// query asyncLoad.progress here for progress (0.0f to 1.0f)
		yield return null;
	}
	
	// loading done ...

	Debug.Log("Scene Loaded");
	
	// note: see Scene.cs for more info on how to load/unload Scenes
	
	/* */

    /* case: extract and use audioclip */
    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    
	// load content from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "commonaudio"
    AssetBundleCreateRequest audioPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commonaudio"));

    yield return new WaitWhile(() => audioPackResult.isDone == false);

    AssetBundle audioPack = audioPackResult.assetBundle as AssetBundle;
    AudioClip   audioClip = audioPack.LoadAsset<AudioClip>("audioname");

    if (audioClip == null) { Debug.Log("Error Loading Audio"); }

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. audioPack.Unload(true);
    audioPack.Unload(false);

    audioSource.clip = audioClip;
    audioSource.Play();
    /* */


    /* case: extract and use Sprite */
    SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
    
	// load content from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "commontextures"
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commontextures"));

    yield return new WaitWhile(() => texturesPackResult.isDone == false);

    AssetBundle texturesPack = texturesPackResult.assetBundle as AssetBundle;
    Sprite      spr          = texturesPack.LoadAsset<Sprite>("spritename");

    if (spr == null) { Debug.Log("Error Loading Sprite"); }

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. texturesPack.Unload(true);
    texturesPack.Unload(false);

    sprRenderer.sprite = spr;
    /* */


    /* case: extract and use Texture2D as sprite */
    SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
 	
	// load content from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "commontextures"   
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commontextures"));

    yield return new WaitWhile(() => texturesPackResult.isDone == false);

    AssetBundle texturesPack = texturesPackResult.assetBundle as AssetBundle;
    Texture2D   tex          = texturesPack.LoadAsset<Texture2D>("texturename");

    if (tex == null) { Debug.Log("Error Loading Texture"); }

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. texturesPack.Unload(true);
    texturesPack.Unload(false);

    Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    sprRenderer.sprite = spr;
    /* */


    /* case: extract and use a sprite from a SpriteAtlas */
    SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
    
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commontextures"));

    yield return new WaitWhile(() => texturesPackResult.isDone == false);

    AssetBundle texturesPack = texturesPackResult.assetBundle as AssetBundle;
    SpriteAtlas sprAtlas     = texturesPack.LoadAsset<SpriteAtlas>("spriteatlasname");

    if (sprAtlas == null) { Debug.Log("Error Loading Sprite Atlas"); }

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. texturesPack.Unload(true);
    texturesPack.Unload(false);

    Sprite spr = sprAtlas.GetSprite("spritenameinatlas");

    sprRenderer.sprite = spr;
	/* */


    /* case: extract and read Text content */
    AssetBundleCreateRequest textdataResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commondata"));

    yield return new WaitWhile(() => textdataResult.isDone == false);

    AssetBundle textPack = textdataResult.assetBundle as AssetBundle;
    TextAsset   textData = textPack.LoadAsset<TextAsset>("textFilename");

    if (textData == null) { Debug.Log("Error Loading Text Data"); }

    string textContent = textData.text; // read text
    Debug.Log( textContent ); 

    // OR read & parse text data as Json 
    // e.g. MyJsonObjectType jsonObj = JsonUtility.FromJson<MyJsonObjectType>(textData.text);
    // See Json.cs for better example

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. textPack.Unload(true);
    textPack.Unload(false);
	/* */
	
	
	/* case: extract and use Font */
    Text textComponent = gameObject.AddComponent<Text>();  // Text requires a Canvas to display and is using UnityEngine.UI (See UI.cs for better example)
    
	// load content from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "commonfonts"
    AssetBundleCreateRequest fontPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/commonfonts"));

    yield return new WaitWhile(() => fontPackResult.isDone == false);

    AssetBundle fontPack = fontPackResult.assetBundle as AssetBundle;
    Font        fnt      = fontPack.LoadAsset<Font>("fontname");

    if (fnt == null) { Debug.Log("Error Loading Font"); }

    // note: setting Unload(true) will unload all loaded assets from that bundle
    //       e.g. fontPack.Unload(true);
    fontPack.Unload(false);

    textComponent.font = fnt;
    /* */
	
}


// load Scene synchronous, this will block the main game thread and you have to wait until the asset loads, usually useful for testing

/// Start():

// load scene from an assetBundle file residing in "Assets/StreamingAssets/AssetBundles/StandaloneWindows" folder, named "myscene"
AssetBundle assetPack = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/test"));
string[] scenePaths   = assetPack.GetAllScenePaths();

// load the first scene in the Asset Bundle, usually what you want
SceneManager.LoadScene(Path.GetFileNameWithoutExtension(scenePaths[0]), LoadSceneMode.Single);


/* -----------------------------------------
   Load all files from AssetBundle
----------------------------------------- */ 
/// Class Body:
private int totalAssets;  // total assets to load, make public to be able to reference it (for e.g. loading progress)
private int loadedAssets; // currently loaded asset count, make public to be able to reference it (for e.g. loading progress)
private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>(); // a database of the sounds loaded from the asset pack

/// IEnumerator Start(), IEnumerator MyMethod():
IEnumerator Start()
{
	
	/* case: extract audio files and create AudioClips out of them */
	AssetBundleCreateRequest audioPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/StandaloneWindows/stage1sounds"));
	
	if (audioPackResult == null) { 
		yield break;
	}
	
	while (!audioPackResult.isDone) {
		
		// query audioPackResult.progress here for the load progress of the asset bundle ...
		yield return null;
		
	}

	// asset bundle loading done ...
	
	audioPack = audioPackResult.assetBundle as AssetBundle;

	// get number of files in assetbundle by quering the length of the string array returned by GetAllAssetNames()
	totalAssets = audioPack.GetAllAssetNames().Length;
	
	// run through all files in assetbundle and load them as AudioClips
	foreach (string currentAsset in audioPack.GetAllAssetNames()) {
		
		AssetBundleRequest request = audioPack.LoadAssetAsync<AudioClip>(currentAsset);
		
		while (!request.isDone) {
			
			// query request.progress here for the load progress of the asset bundle and request.asset.name to get the currently loading asset name...
			yield return null;
			
		}
		
		// create a new AudioClip from loaded sound asset
		AudioClip audioClip = (AudioClip)request.asset;
		
		Debug.Log(GetType()+": Loaded asset "+request.asset.name);

		if (audioClip == null) {
			// could not load asset ...
		} else {
			// successful loading of asset, increment loadedAssets
			loadedAssets += 1;
		}

		// add the loaded asset by its name and AudioClip reference for later use (for playback, freeing resource from memory etc.)
		sounds.Add(Path.GetFileNameWithoutExtension(currentAsset), audioClip);
		
	}
	
	// loading all assets complete, you can compare loadedAssets vs totalAssets and have an isLoaded flag in order to proceed with the game or take other action ...
	
	
}


/* -----------------------------------------
   Load AssetBundle (WebGL)
----------------------------------------- */ 
// note: WebGL can only load AssetBundles this way

/// Start(), Awake(), MyMethod():
using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(Application.streamingAssetsPath+"AssetBundles/WebGL/packedgameobjects"))
{
	yield return webRequest.SendWebRequest();

	if (webRequest.isNetworkError || webRequest.isHttpError)
	{
		Debug.Log("WebRequest Error");
	} else {

		AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
		
		GameObject  packedGameObject = assetBundle.LoadAsset<GameObject>("TestObject");

		if (packedGameObject == null) { 
			Debug.Log("Error Loading Asset");
		}

		GameObject newObj = GameObject.Instantiate(packedGameObject, Vector3.zero, Quaternion.identity);
		
		// get MyCustomScript component from loaded Asset Bundle
		MyCustomScript scriptInBundle = newObj.GetComponent<MyCustomScript>();
		
		// test value from component in prefab
		Debug.Log("Value in GameObject from AssetBundle is: "+scriptInBundle.test.ToString());
		
	}
}


