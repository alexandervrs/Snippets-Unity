
/**
 * Tileset.cs
 * Tileset related snippets for Unity
 */


/* -----------------------------------------
   Setup Tileset
----------------------------------------- */

/*
	This will setup a Tileset the right way, also eliminating any in-between lines/artifacts between tiles
	
	1. Import Tileset texture as "Sprite (2D & UI)"
	2. Set "Sprite Mode" to "Multiple"
	3. Open "Sprite Editor" and choose "Slice" from the top left
	4. Choose "Type" > "Grid by Cell Size" and edit Grid Size details and then click "Slice" button
	5. Assets > Create > Sprite Atlas and assign your Tileset texture under "Objects for packing" and set "Padding" as much as you need to zoom/scale it, 
	   click "Pack Preview", make sure "Sprite Packer" is enabled (Edit > Project Settings > Editor > Sprite Packer set to Mode: Always Enabled)
	6. In your Scene, right click and 2D Object > Tilemap
	7. Click on the created Grid and change Cell Size to your tileset's cell size 
	   (e.g. 32x32 is X: 0.32, Y: 0.32) (CellSize / PixelsPerUnit), pixels per unit being 100
	8. Window > 2D > Tile Palette, drag the tileset onto the "Tile Palette" window and choose a folder to create the tile assets into
	
*/

