
/**
 * Component.cs
 * Component related snippets for Unity
 */


/* -----------------------------------------
   Create a Class object & call method
----------------------------------------- */

/* Step 1: create your class file as a new C# Script */

// -------------( MyScript.cs )--------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour {

	public class MySubScript {
		
		public bool IsPlaying { get; set; } // returns (gets) or sets the IsPlaying state

		public void Play() {
			this.IsPlaying = true; // update the IsPlaying state of the current object (this MySubScript)
			Debug.Log("Play method called");
		}
		
	}

	public MySubScript Create() {
		MySubScript newObj = new MySubScript();
		newObj.IsPlaying = false;
		return newObj;
	}

}
// -------------( MyClass.cs )--------------

/* Step 2: create your script and attach it to GameObject */

/// Class Body:
public MyScript myScr;

/// Awake(), Start():
myScr = gameObject.GetComponent<MyScript>(); // get the class as component to the gameobject (or AddComponent() if not attached)

/// Start(), Update():
MyScript.MySubScript mySub = myScr.Create(); // create a new object
mySub.Play(); // call the Play() method of that object
Debug.Log(mySub.IsPlaying); // prints the playing state of that object
mySub.IsPlaying = false;  // sets the playing state of that object

