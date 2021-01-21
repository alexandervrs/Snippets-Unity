
/**
 * Sprite.cs
 * Sprite & Sprite Renderer related snippets for Unity
 */

/* using */
using UnityEngine;


/* 
	Important Note:
		
	Using "material" to change properties like color/alpha will cause Unity to create a copy of that material 
	so you can commit your changes without affecting other objects using the same material

*/

Material mMaterial;

void Start() 
{
	// get and store the material index
	mMaterial = GetComponent<Renderer>().material;
}

void OnDestroy()
{
	// destroy the material on object's Destroy event
	Destroy(mMaterial);
}
		
/*		
	You'd also need to use Resources.UnloadUnusedAssets() when done with the scene to remove those copies from memory if they exist

	Alternatively you could use sharedMaterial which does not need Resources.UnloadUnusedAssets() 
	but will also affect other objects' materials
*/


/* -----------------------------------------
   Set the Sprite's properties
----------------------------------------- */

// set the sprite's transparency
gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.4f); // (r,g,b,a), 0.4f is the new alpha value

// get the sprite's transparency
float alpha = gameObject.GetComponent<SpriteRenderer>().material.color.a;

// flip the sprite horizontally
gameObject.GetComponent<SpriteRenderer>().flipX = true;

// flip the sprite vertically
gameObject.GetComponent<SpriteRenderer>().flipY = true;


/* -----------------------------------------
   Create new Sprite from Texture
----------------------------------------- */
/// Start():
Texture2D spriteTexture;
// create a new sprite with texture, of size (0,0,w,h) and pivot set to center (0.5) on both axes
Sprite spr = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f));

SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
sprRenderer.sprite = spr;


/* -----------------------------------------
   Depth Sort Sprite by Y Position
----------------------------------------- */
/// Class Body:
// note: target is used in case the GameObject has child GameObjects that are in a different height than the root one but need the same sort order
public Transform target; 

public float targetOffset = 0;

/// Start():
sprRenderer = gameObject.GetComponent<SpriteRenderer>();

/// Update():
if (target == null) {
    target = gameObject.transform;
}
sprRenderer.sortingOrder = (int)((target.position.y * -sprRenderer.sprite.pixelsPerUnit) + targetOffset);


/* -----------------------------------------
   Destroy Sprite
----------------------------------------- */
// unassign sprite and destroy it
sprRenderer.sprite = null;
Object.Destroy(spr);

