
/**
 * TextMeshPro.cs
 * TextMeshPro related snippets for Unity
 */

/* using */
using TMPro;


/* -----------------------------------------
   Setup TextMeshPro Font
----------------------------------------- */

/*

	1. Add font file in Unity (.otf, .ttf)
	2. Create new Font Asset with tool:
	   Window > TextMeshPro > Font Asset Creator
	3. Select Font, change Font Settings, Generate Font Atlas and Save
	
*/


/* -----------------------------------------
   Create TextMeshPro Material Preset
----------------------------------------- */

/*
	1. Click on the Font Asset and expand it using the arrow at the left
	2. Click and select the SDF Material
	3. Ctrl+D to duplicate it, the material needs to be named as:
	   "<Font Name> <Material Name>" in order to appear under the Material Preset
	   dropdown in the Inspector
*/


/* -----------------------------------------
   Get Rendering Info
----------------------------------------- */

// get textmesh component
TextMeshPro textMesh = GetComponent<TextMeshPro>(); // or TextMeshProUGUI for UI Text Mesh

// return the rendered width and height of a TextMesh object
Vector2 renderedValues = textMesh.GetRenderedValues(true); // true is onlyVisibleCharacters
