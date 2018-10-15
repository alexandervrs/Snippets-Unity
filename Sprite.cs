
/**
 * Sprite.cs
 * Sprite related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Create new Sprite from Texture
----------------------------------------- */
// Start():
Texture2D spriteTexture;
// create a new sprite with texture, of size (0,0,w,h) and pivot set to center (0.5) on both axes
Sprite spr = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f));

SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
sprRenderer.sprite = spr;


/* -----------------------------------------
   Destroy Sprite
----------------------------------------- */
// unassign sprite and destroy it
sprRenderer.sprite = null;
Object.Destroy(spr);

