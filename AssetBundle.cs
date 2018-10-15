
/**
 * AssetBundle.cs
 * AssetBundle related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.U2D; // for SpriteAtlas
using System.IO; // for Path


/* -----------------------------------------
   Setup Asset Bundle
----------------------------------------- */

/*

	1. Choose AssetBundle name at the bottom of the Inspector for each asset
	2. Compile AssetBundle for the target Platform using AssetBundle Browser or Editor script
	3. Load assets from bundle and use them
	
*/


/* -----------------------------------------
   Load files from AssetBundle
----------------------------------------- */ 
IEnumerator Start()
{

    /* case: extract and use audioclip */
    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    
	// load content from an assetBundle file residing in "Resources" folder, named "commonaudio"
    AssetBundleCreateRequest audioPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commonaudio"));

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
    
	// load content from an assetBundle file residing in "Resources" folder, named "commontextures"
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commontextures"));

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
 	
	// load content from an assetBundle file residing in "Resources" folder, named "commontextures"   
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commontextures"));

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
    
    AssetBundleCreateRequest texturesPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commontextures"));

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
    AssetBundleCreateRequest textdataResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commondata"));

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
    
	// load content from an assetBundle file residing in "Resources" folder, named "commonfonts"
    AssetBundleCreateRequest fontPackResult = AssetBundle.LoadFromFileAsync(Path.Combine(Application.dataPath, "Resources/Windows/commonfonts"));

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


/* -----------------------------------------
   Create AssetBundle (Editor)
----------------------------------------- */ 

// note: place script in "Editor" folder
//       also add more menu items for other platforms
//       Different Asset Bundles need to be generated for each target platform

[MenuItem("Tools/Assets/Build Asset Bundle/Windows")]
static void BuildAssetBundlesWin()
{
    string assetBundleDirectory = "Assets/Resources/Windows/";

    if (!Directory.Exists(assetBundleDirectory)) {
        Directory.CreateDirectory(assetBundleDirectory);
    }

    BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows);
}

