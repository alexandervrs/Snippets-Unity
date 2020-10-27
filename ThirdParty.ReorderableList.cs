
/**
 * ThirdParty.ReorderableList.cs
 * ReorderableList related snippets for Unity
 */

/* using */
using UnityEngine;
using Malee.List; //  for ReorderableList


/*
	note: Import the ReorderableList package https://github.com/cfoulston/Unity-Reorderable-List (free) in your Unity project
*/


/* -----------------------------------------
   Setup & Access Reorderable List
----------------------------------------- */
/// Class Body:
[Reorderable]
public ListOfItems myReorderableList;

[System.Serializable]
public class ListOfItems : ReorderableArray<MyItemClass>{};

[System.Serializable]
public class MyItemClass
{
	public float  someValue = 1.0f;
	public string someOtherValue = "test";
}

/// Start(), Update():
// access list items in a loop
foreach (MyItemClass item in myReorderableList) {
	Debug.Log(item.someValue);
	/// ...
}

