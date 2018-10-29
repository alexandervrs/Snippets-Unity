
/**
 * SpriteFont.cs
 * Sprite/Bitmap font related snippets for Unity
 */


/* -----------------------------------------
   Setup Sprite Font
----------------------------------------- */

/*
	
	1.  Create your font image, preferably in a grid, character order should be:
	    !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~
	2.  Download Shoebox from https://renderhjs.net/shoebox/ (free, requires Adobe AIR installed https://get.adobe.com/air/ )
	3.  Open the app and select "GUI" from the tab, drag and drop your sprite/bitmap font image into the "T" area
	    Your font needs to be large enough and have transparency for the characters to be auto-detected
	4.  Select "Auto fit Settings"
	5.  Click "Advanced" tab and change "File Format Outer" & "File Name" to both just your font's name 
	6.  Increase Tex Padding to 4 (or larger based on the amount of zoom the font is expected to make), change Tex Max Size if you require it
	7.  Click Apply, then close the settings dialog
	8.  Click "Save Font" OR press Enter to save the font files (if your font is too large it may glitch the Shoebox UI)
	9.  Import the BitmapFontImporter https://github.com/litefeel/Unity-BitmapFontImporter (free) in your Unity project
	10. Open the .fnt file with a text editor and change the "file" section from something like file="C:\Users\<yourusername>\Desktop\myfont.png"
	    to just file="myfont.png" so the importer can find it in the project "Assets" folder
	11. Drag and drop the .fnt & generated .png file to import and create the font file
	12. Test it out GameObject > UI > Text and choose your font in the Inspector
	13.	You can rebuild an existing font file anytime by selecting: Assets > Bitmap Font > Rebuild Bitmap Font
	
*/

