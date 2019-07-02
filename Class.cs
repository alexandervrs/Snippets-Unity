
/**
 * Class.cs
 * Class related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Fluent interface, Method Chaining
----------------------------------------- */

/* Step 1: create a class for the object contents (optional, only if you use SetDescription()/GetDescription() below) */
public class MyClassObjectContents
{
    public string description;
	public float  someOtherValue;
	// ...
	
	public MyClassObjectContents() {
        /// constructor
		/// ...
    }
}


/* Step 2: create a class for the chaining methods */
public static class ChainedClass 
{

	// optional (only if you use SetDescription()/GetDescription() below)
    static MyClassObjectContents item = new MyClassObjectContents();

    // this method can chain, sets the value for transform position, scale all in one
    public static GameObject SetPositionAndScale(this GameObject thisGameObject, Vector3 position, Vector3 scale)
    {

        thisGameObject.transform.position   = new Vector2(position.x, position.y);
        thisGameObject.transform.localScale = scale;

        return thisGameObject; // required, return this GameObject back for chaining (you can return anything back and chain that as well)
    }

    // this method can chain, sets the value for the description
    public static GameObject SetDescription(this GameObject thisGameObject, string desc)
    {
        item.description = desc;

        return thisGameObject; // required, return this GameObject back for chaining (you can return anything back and chain that as well)
    }

	// this method does not chain but returns an item value (description)
    public static string GetDescription()
    {
        return item.description;
    }

}

/* Step 3: use the new methods */
gameObject.SetDescription("some description").SetPositionAndScale(new Vector3(1, -1, 0), new Vector3(2, 2, 2));
