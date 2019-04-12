
/**
 * ReorderableList.cs
 * ReorderableList related snippets for Unity
 */

/* using */
using UnityEngine;
using Assets.ReorderableList; //  for ReorderableList


/*
	note: Import the ReorderableList package https://github.com/cfoulston/Unity-Reorderable-List (free) in your Unity project
*/


/* -----------------------------------------
   Setup & Access Reorderable List
----------------------------------------- */
/// Class Body:
public ListOfItems myReorderableList;

[System.Serializable]
public class ListOfItems : ReorderableList.Of<MyItemClass>{};

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

