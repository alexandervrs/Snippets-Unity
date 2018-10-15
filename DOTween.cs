
/**
 * DOTween.cs
 * DOTween related snippets for Unity
 */

/* using */
using DG.Tweening;
using TMPro; // for TextMeshPro only


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
   Tween Transform
----------------------------------------- */

// animate move
gameObject.transform.DOMove(new Vector3(
	gameObject.transform.position.x+1.0f, 
	gameObject.transform.position.y, 
	gameObject.transform.position.z
), 1.5f).SetEase(Ease.OutSine);

// animate rotate
gameObject.transform.DORotate(new Vector3(
	gameObject.transform.rotation.x-180.0f, 
	gameObject.transform.rotation.y+180.0f, 
	gameObject.transform.rotation.z-180.0f
), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.OutSine);

// animate scale
gameObject.transform.DOScale(new Vector3(
	gameObject.transform.localScale.x+1.0f, 
	gameObject.transform.localScale.y+1.0f, 
	gameObject.transform.localScale.z+1.0f
), 1.5f).SetEase(Ease.OutSine);

// animate alpha
gameObject.transform.GetComponent<Renderer>().material.DOFade(1.5f, 0.0f).SetEase(Ease.OutSine).OnComplete(() => {
	// fade complete ...
});



/* -----------------------------------------
   Tween Path
----------------------------------------- */

// animate across path
List<Vector3> pathPoints = new List<Vector3>();

pathPoints.Insert(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
pathPoints.Insert(1, new Vector3(1.44f,  -1.24f,  0.0f));
pathPoints.Insert(2, new Vector3(0.59f,  -2.7f,   0.0f));
pathPoints.Insert(3, new Vector3(-1.98f, -2.37f,  0.0f));
pathPoints.Insert(4, new Vector3(-4.7f,   0.06f,  0.0f));
pathPoints.Insert(5, new Vector3(-5.7f,   1.06f,  0.0f));
pathPoints.Insert(pathPoints.Count, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));

gameObject.transform.DOPath(pathPoints.ToArray(), 2.0f, PathType.CatmullRom, PathMode.Full3D, 10, null).SetEase(Ease.InOutSine).SetDelay(1.0f).OnComplete(() => {
	// path complete ...
});


/* -----------------------------------------
   Tween Value
----------------------------------------- */
// tween custom value, 1f is start value, 0f is end value, 3f is duration
float valueToChange = 1f;
Tween tweenID = DOTween.To(() => 1f, x => valueToChange = x, 0f, 3f).SetEase(Ease.OutSine);


/* -----------------------------------------
   Loop Tween
----------------------------------------- */
Tweener thisTween = gameObject.transform.DOMove(new Vector3(
	gameObject.transform.position.x, 
	gameObject.transform.position.y+5f,
	gameObject.transform.position.z
), duration).SetEase(Ease.Linear).SetDelay(1f).OnComplete(()=> { 

	gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-5f, gameObject.transform.position.z);
	gameObject.moveTween.Restart(false); // setting false will ignore the SetDelay()

});

gameObject.moveTween = thisTween;


/* -----------------------------------------
   Tween Sequence
----------------------------------------- */

// play sequence
Sequence timeline = DOTween.Sequence();

timeline.InsertCallback(2, () => {  // callback
	// Sequence Callback at 2s ..." 
});
timeline.Insert(3, gameObject.transform.DOScale(0.4f, 0.3f).SetEase(Ease.InBack) ); // inline expression
timeline.SetLoops(-1); // loop endlessly
timeline.Play();


/* -----------------------------------------
   Tween Text (TextMeshPro)
----------------------------------------- */

// animate letters
gameObject.transform.GetComponent<TextMeshPro>().DOText("10000", 1.5f, true, ScrambleMode.Numerals, "0123456789").SetEase(Ease.OutSine);

// animate numeric value
float targetValue      = 10000f;
float currentTextValue = 0.0f;
DOTween.To(() => 0.0f, x => currentTextValue = x, targetValue, 2.0f).SetEase(Ease.OutQuint).OnUpdate(() => {

	// round number and update text value on each frame
	gameObject.transform.GetComponent<TextMeshPro>().SetText(Mathf.Ceil(currentTextValue).ToString());

});

// animate face color
gameObject.transform.GetComponent<TextMeshPro>().DOFaceColor(Color.yellow, 1.5f);

// animate outline color
gameObject.transform.GetComponent<TextMeshPro>().DOOutlineColor(Color.red, 1.5f);

// animate alpha
gameObject.transform.GetComponent<TextMeshPro>().DOFade(0.0f, 1.5f);

/* -----------------------------------------
   Tween Playback Control
----------------------------------------- */

Tween    tween = // ... DOTween 
Sequence tween = // ... or Sequence 

// add a delay before starting tween/sequence
tween.SetDelay(1.0f);

// plays the tween/sequence
tween.Play();

// pauses the tween/sequence
tween.Pause();

// stop the tween/sequence
tween.Kill();

// stop the tween/sequence but finish it first
tween.Kill(true);

// reverse the tween/sequence playback direction
tween.Flip();

// restart the tween/sequence
tween.Restart();

// restart the tween/sequence and includes any delay with SetDelay()
tween.Restart(true);

// rewinds the tween/sequence and pauses it
tween.Rewind();

// rewinds the tween/sequence, pauses it and includes any delay with SetDelay()
tween.Rewind(true);


/* -----------------------------------------
   Tween Effects
----------------------------------------- */

// animate shake effect
gameObject.transform.DOShakePosition(2.0f, 0.2f, 110, 90, false, true);

// animate rubber scale effect
gameObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 2.5f, 10, 1).SetEase(Ease.OutElastic);

