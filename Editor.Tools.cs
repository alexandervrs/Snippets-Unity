
/**
 * Editor.Tools.cs
 * Editor Tools related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Change Active Editor Tool
----------------------------------------- */
Tools.current = Tool.Move;

/*
	Tools:
		None, View, Move, Rotate, Scale, Rect, Transform
*/

