
/**
 * Color.cs
 * Color related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Create Color
----------------------------------------- */
// create new Color (r,g,b,a) [0 to 1]
Color newColor = new Color(0.2f, 0.3f, 0.6f, 0.8f);

// create new Color in 32bit format (r,g,b,a) [0 to 255]
Color32 newColor32 = new Color32(100, 149, 237, 255);

// create new Color out of Hex
Color hexColor = new Color();
ColorUtility.TryParseHtmlString("#db397f", out hexColor);


/* -----------------------------------------
   Create Gradient
----------------------------------------- */
/// Class Body:
Gradient colorGradient;
GradientColorKey[] gradientColorKey;
GradientAlphaKey[] gradientAlphaKey;

/// Start(), Update():
gradientColorKey = new GradientColorKey[2];
gradientColorKey[0].color = Color.red;
gradientColorKey[0].time  = 0.0f;
gradientColorKey[1].color = Color.blue;
gradientColorKey[1].time  = 1.0f;

gradientAlphaKey = new GradientAlphaKey[2];
gradientAlphaKey[0].alpha = 1.0f;
gradientAlphaKey[0].time  = 0.0f;
gradientAlphaKey[1].alpha = 0.0f;
gradientAlphaKey[1].time  = 1.0f;

colorGradient = new Gradient();
colorGradient.SetKeys(gradientColorKey, gradientAlphaKey);

