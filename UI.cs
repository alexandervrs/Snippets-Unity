
/**
 * UI.cs
 * UI related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.UI;


/* -----------------------------------------
   Setup UI Text Font
----------------------------------------- */

/*

	1. Drag and drop a font file (.otf, .ttf) to add it in Unity
	2. In the Import Settings, click on the Font asset 
	3. Change Rendering Mode to "Smooth", Character to "Unicode" & Font Size to something large like 100. Click Apply
	4. Move the asset file under "Resources" folder or include in an Asset Bundle (See AssetBundle.cs)
	
*/


/* -----------------------------------------
   Create a UI Text
----------------------------------------- */
/// Start():

// 1. add a Gameobject with a Canvas component
GameObject textCanvas = new GameObject();
textCanvas.name = "MyTextCanvas";
textCanvas.transform.position = new Vector3(0, 0, 0);
textCanvas.transform.localScale = new Vector3(1, 1, 1);

Canvas canvasContent = textCanvas.AddComponent<Canvas>();

// set the Canvas to render its contents to World Space
canvasContent.renderMode = RenderMode.WorldSpace;

// OR create fixed to the Camera, useful for HUD/UI
//canvasContent.renderMode  = RenderMode.ScreenSpaceCamera;
//canvasContent.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

// 2. add a CanvasScaler component to scale the text properly
CanvasScaler canvasScaler = textCanvas.AddComponent<CanvasScaler>();
canvasScaler.dynamicPixelsPerUnit = 100; // adjust based on font size

// 3. create the actual Text GameObject
GameObject textObject = new GameObject();
textObject.name = "MyText";
textObject.transform.position   = new Vector3(0, 0, 0);
textObject.transform.localScale = new Vector3(1, 1, 1);

Text textContent = textObject.AddComponent<Text>();

// 4. change Text options
textContent.text      = "Sample Text"; // the actual text string
textContent.font      = Resources.Load<Font>("YourFontName");
// OR use the built-in Arial font for quick testing
// textContent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
textContent.fontSize  = 1; // note: this is not your regular fontSize, it's more of your font maxTextureSize
textContent.color     = Color.white;
textContent.alignment = TextAnchor.MiddleCenter;

// note: change localscale to scale down the font size, scaling up more than the imported Font Size will result in blurry text
//textObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

// 5. parent Text to Canvas
textObject.transform.SetParent(textCanvas.transform, true);

