
/**
 * SpriteAtlas.cs
 * SpriteAtlas related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.U2D; // for SpriteAtlas


/* -----------------------------------------
   Setup Sprite Atlas
----------------------------------------- */

/*

	1. Right click on Project Asset tree and choose "Create > Sprite Atlas"
	2. Under "Objects for Packing" add the Textures to pack in the Atlas
	   Tip: You can drag & drop multiple textures by dragging them over the "Objects for Packing" title label
	
*/


/* -----------------------------------------
   Get Sprite from Atlas
----------------------------------------- */
// get first sprite of name (cloned)
SpriteAtlas sprAtlas;
Sprite spr = sprAtlas.GetSprite("spritenameinatlas");

SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
sprRenderer.sprite = spr;

// get all sprites in the atlas (cloned)
SpriteAtlas sprAtlas;
Sprite[] spritesInAtlas = new Sprite[sprAtlas.spriteCount];
sprAtlas.GetSprites(spritesInAtlas);

Dictionary<string, Sprite> spritesInAtlasIndexed = new Dictionary<string, Sprite>();
foreach (Sprite sprite in spritesInAtlas) {
    // add sprites in an index by name ...
    spritesInAtlasIndexed.Add(sprite.name, sprite);
}

SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
sprRenderer.sprite = spritesInAtlasIndexed["spritenameinatlas(Clone)"];


/* -----------------------------------------
   Get info from Atlas
----------------------------------------- */
SpriteAtlas sprAtlas;

// returns the sprite count in the atlas
sprAtlas.spriteCount;

