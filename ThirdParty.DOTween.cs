
/**
 * ThirdParty.DOTween.cs
 * DOTween related snippets for Unity
 *
 * https://github.com/Demigiant/dotween
 * https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416
 */

/* using */
using DG.Tweening;
using TMPro;       // for TextMeshPro only
using Spine.Unity; // for SkeletonRenderer only


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

// animate position
gameObject.transform.DOMove(new Vector3(
	gameObject.transform.position.x+1.0f, 
	gameObject.transform.position.y, 
	gameObject.transform.position.z
), 1.5f).SetEase(Ease.OutSine);

// animate rotation
gameObject.transform.DORotate(new Vector3(
	gameObject.transform.rotation.x-180.0f, 
	gameObject.transform.rotation.y+180.0f, 
	gameObject.transform.rotation.z-180.0f
), 1.5f, RotateMode.WorldAxisAdd).SetEase(Ease.OutSine);
// note: RotateMode: FastBeyond360 is fast way to do simple rotations, LocalAxisAdd & WorldAxisAdd are relative like you are using Rotate(), sometimes they are better e.g. rotating around a 3D object

// animate scale
gameObject.transform.DOScale(new Vector3(
	gameObject.transform.localScale.x+1.0f, 
	gameObject.transform.localScale.y+1.0f, 
	gameObject.transform.localScale.z+1.0f
), 1.5f).SetEase(Ease.OutSine);

// animate alpha
// use either <Renderer>, <SpriteRenderer>, <MeshRenderer>, <Image> based on what you want to fade
gameObject.transform.GetComponent<Renderer>().material.DOFade(0.5f, 1.2f).SetEase(Ease.OutSine).OnComplete(() => { // DOFade(endValue, duration)
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
   Tween Camera
----------------------------------------- */

// zoom in Orthographic camera to orthographic size: 3
gameObject.GetComponent<Camera>().DOOrthoSize(3, 0.7f).SetEase(Ease.InOutSine);

// zoom in Perspective camera to fieldOfView: 4
gameObject.GetComponent<Camera>().DOFieldOfView(4, 0.7f).SetEase(Ease.InOutSine);


/* -----------------------------------------
   Tween Value
----------------------------------------- */

// tween custom value, 1f is start value, 0f is end value, 3f is duration
float valueToChange = 1f;
Tweener tweenID = DOTween.To(() => 1f, x => valueToChange = x, 0f, 3f).SetEase(Ease.OutSine);


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

/* 
	note:  Tweens inside InsertCallback() do not automatically cleanup & need to be done manually
		   You may keep the Tweeners in a list and iterate to kill the tweens e.g.

		   It's also good practice to kill the sequence as well in case the object gets destroyed unexpectedly

			/// Class Body:
			private Sequence       sequence;
			private List<Tweener>  tweeners  = new List<Tweener>();

			/// OnDestroy():
			if (sequence) {
				sequence.Kill();
				sequence = null;
			}

			foreach (Tweener thisTweener in tweeners) {
				thisTweener.Kill();
			}

			tweeners.Clear();
			tweeners = null;

*/

// create sequence
Sequence sequence = DOTween.Sequence();

// insert a tween in a keyframe, Insert only accepts DOTween Tweeners
// note: tweens inside Insert() are managed by the Sequence and cleanup automatically
sequence.Insert(3, gameObject.transform.DOScale(0.4f, 0.3f).SetEase(Ease.InBack) );

// insert a callback in a keyframe, can be any kind of code/action
sequence.InsertCallback(2.3f, () => { 
	// Sequence Callback at 2.3s ..." 
});

// execute a callback on complete of one loop cycle
sequence.OnStepComplete(() => { 
	// Sequence completed one loop cycle ...
});

// execute a callback on complete of the entire sequence
sequence.OnComplete(() => { 
	// Sequence on completed (includes loops) ...
});

sequence.SetLoops(-1); // loop endlessly
sequence.Play(); // play the entire sequence


/* -----------------------------------------
   Tween Text (TextMeshPro)
----------------------------------------- */

// animate letters
gameObject.transform.GetComponent<TextMeshPro>().DOText("10000", 1.5f, true, ScrambleMode.Numerals, "0123456789").SetEase(Ease.OutSine);

// animate numeric value
float targetValue      = 10000f;
float currentTextValue = 0.0f; // also initial value, you may also do DOTween.To(() => 1400f ... to provide a custom initial value
DOTween.To(() => currentTextValue, x => currentTextValue = x, targetValue, 2.0f).SetEase(Ease.OutQuint).OnUpdate(() => { // 2.0f is tween duration

	// round number and update text value on each frame
	gameObject.transform.GetComponent<TextMeshPro>().SetText(Mathf.Ceil(currentTextValue).ToString());

});

// animate face color
gameObject.transform.GetComponent<TextMeshPro>().DOFaceColor(Color.yellow, 1.5f);

// animate outline color
gameObject.transform.GetComponent<TextMeshPro>().DOOutlineColor(Color.red, 1.5f);

// animate alpha
gameObject.transform.GetComponent<TextMeshPro>().DOFade(0.0f, 1.5f);


/* --------------------------------------------
   Tween Shader Property
-------------------------------------------- */

/// Class Body:
Shader   grayscaleShader;
Material grayscaleMaterial;

/// Start():
grayscaleShader   = Shader.Find("VisualFX/PostProcess/Grayscale");
grayscaleMaterial = new Material( grayscaleShader );

/// Start(), Update():
float targetValue  = 1.0f;
float currentValue = 0.0f; // also initial value, you may also do DOTween.To(() => 1400f ... to provide a custom initial value
DOTween.To(() => currentValue, x => currentValue = x, targetValue, 2.0f).SetEase(Ease.OutSine).OnUpdate(() => { // 2.0f is tween duration
	
	grayscaleMaterial.SetFloat("_FXAmount", currentValue);

});


/* --------------------------------------------
   Tween Skeleton Color/Alpha (Spine)
-------------------------------------------- */

float targetValue  = 0.0f;
float currentAlpha = 1.0f; // also initial value, you may also do DOTween.To(() => 1400f ... to provide a custom initial value
DOTween.To(() => currentAlpha, x => currentAlpha = x, targetValue, 1.0f).SetEase(Ease.OutQuint).OnUpdate(() => { // 1.0f is tween duration
	
	gameObject.GetComponent<SkeletonRenderer>().skeleton.SetColor(new Color(1, 1, 1, currentAlpha));

});


/* -----------------------------------------
   Tween Playback Control
----------------------------------------- */

Tweener  tween = // ... DOTween 
Sequence tween = // ... or Sequence 

// add a delay before starting tween/sequence
tween.SetDelay(1.0f);

// plays/resumes the tween/sequence
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

// plays/resumes the tween/sequence backwards
tween.PlayBackwards();

// set ID to manipulate single tween or mass manipulate tweeners 
tween.SetId("MyID");

// play/resume all tweens with that ID
DOTween.Play("MyID");

// pause all tweens with that ID
DOTween.Pause("MyID");

// stops all tweens with that ID
DOTween.Kill("MyID");

// check if Tweener/Sequence is playing, you may also check e.g. DOTween.IsTweening(transform.DOMoveY(2f, 1.2f))
DOTween.IsTweening(tween);

// kill all tweens, and return how many were killed
int killed = DOTween.KillAll();


/* -----------------------------------------
   Tween Effects
----------------------------------------- */

// animate jump effect
// 1.2f is jump power, 1 is number of jumps
gameObject.transform.DOJump(new Vector3(2, 0, 0), 1.2f, 1, 0.5f).SetEase(Ease.InOutSine);

// animate spiral effect
// 0.3f is the offset from center that will be reached, null can be a Vector axis (e.g. Vector3.left) in order to constrain the animation to an axis
gameObject.transform.DOSpiral(2, null, SpiralMode.Expand, 0.3f, 10, 0).SetEase(Ease.InOutSine); 

// animate shake effect
gameObject.transform.DOShakePosition(2.0f, 0.2f, 110, 90, false, true);

// animate rubber scale effect
gameObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 2.5f, 10, 1).SetEase(Ease.OutElastic);


/* -----------------------------------------
   Global Settings
----------------------------------------- */

// enable safe mode, cleans up tweens and checks first if object/component exists
DOTween.useSafeMode = true;

// change the tweener & sequence capacity so DOTween doesn't need to adjust that every time the max is exceeded
DOTween.SetTweensCapacity(200, 50);


/* -----------------------------------------
   Get Info
----------------------------------------- */

// returns the total number of active/playing tweens
int activeTweens = DOTween.TotalPlayingTweens();

// return a list of all the playing tweens
int playingTweens = DOTween.PlayingTweens();

// return a list of all the paused tweens
int pausedTweens = DOTween.PausedTweens();