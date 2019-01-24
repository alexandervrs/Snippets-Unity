
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

// create fixed to the Camera, useful for HUD/UI
canvasContent.renderMode  = RenderMode.ScreenSpaceCamera;
canvasContent.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

// OR set the Canvas to render its contents to World Space
// canvasContent.renderMode = RenderMode.WorldSpace;

// 2. add a CanvasScaler component to scale the text on the screen properly
CanvasScaler canvasScaler         = textCanvas.AddComponent<CanvasScaler>();
canvasScaler.dynamicPixelsPerUnit = 100; // adjust based on font size
canvasScaler.uiScaleMode          = CanvasScaler.ScaleMode.ScaleWithScreenSize; // set to scale/position with screen size
canvasScaler.referenceResolution  = new Vector2(3840, 2160); // reference screen size
canvasScaler.matchWidthOrHeight   = 1f; // ratio to match reference resolution width or height

// 3. create the actual Text GameObject
GameObject textObject = new GameObject();
textObject.name = "MyText";
textObject.transform.position   = new Vector3(0, 0, 0);
textObject.transform.localScale = new Vector3(1, 1, 1);

Text textContent = textObject.AddComponent<Text>();

// 4. change Text options
textContent.text = "Sample Text"; // the actual text string
textContent.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // use the built-in Arial font for quick testing
// textContent.font      = Resources.Load<Font>("YourFontName"); // OR use your own Font 
textContent.fontSize  = 1; // note: this is not your regular fontSize, it's more of your font maxTextureSize, use scaling to scale down the text
textContent.color     = Color.white;
textContent.alignment = TextAnchor.MiddleLeft;
textContent.horizontalOverflow = HorizontalWrapMode.Overflow; // allow showing text out of rectTransform bounds
textContent.verticalOverflow   = VerticalWrapMode.Overflow;   // allow showing text out of rectTransform bounds

// 5. parent Text to Canvas
textObject.transform.SetParent(textCanvas.transform, true);

// 6. adjust rect transform (for RenderMode.ScreenSpaceCamera, AFTER parenting to Canvas)
RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
textRectTransform.anchoredPosition = new Vector2(60, -140); // add some offset to the anchored position
textRectTransform.anchorMax = new Vector2(0, 1); // set the anchor to top-left
textRectTransform.anchorMin = new Vector2(0, 1); // set the anchor to top-left
textRectTransform.pivot = new Vector2(0, 0);     // set the rect pivot to lef-top
textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);
textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,   100);

// 7. set the scale of the gameObject and RectTransform (AFTER parenting to Canvas)
// note: change localscale to scale down the font size, scaling up more than the imported Font Size will result in blurry text
textObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
textObject.transform.localScale = new Vector3(1f, 1f, 1f);

