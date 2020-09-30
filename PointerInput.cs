
/**
 * PointerInput.cs
 * Pointer/Mouse Input related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.EventSystems; // for PointerEventData


/* -----------------------------------------
   Pointer/Mouse Control
----------------------------------------- */

/*

	note: Your MonoBehaviour Script must also implement the according Pointer interfaces
		  e.g. if you use OnPointerDown() and OnPointerExit(), your class must look like:

		  public class MyClassName : MonoBehaviour, IPointerDownHandler, IPointerExitHandler

		  Also, there should be an "EventSystem" GameObject in your Scene

*/

public void OnPointerEnter(PointerEventData eventData) 
{
	// mouse entered the object ...
}

public void OnPointerExit(PointerEventData eventData) 
{
	// mouse exited the object ...
}

public void OnPointerDown(PointerEventData eventData) 
{
	if (eventData.button == PointerEventData.InputButton.Left) {
		// mouse left clicked the object ...
	}
}


/* -----------------------------------------
   Handle Cursor
----------------------------------------- */
// hide the mouse cursor
Cursor.visible = false;

// lock the mouse cursor to the center of the view
Cursor.lockState = CursorLockMode.Locked;

// release the mouse cursor
Cursor.lockState = CursorLockMode.None;

