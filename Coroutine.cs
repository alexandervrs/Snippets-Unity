
/**
 * Coroutine.cs
 * Coroutine related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Execute Coroutine Once
----------------------------------------- */
/// Class Body:
// define the coroutine

IEnumerator ExecuteDelayedOnce() {

    yield return new WaitForSeconds(2.0f);
    // execute some code after 2 seconds here ...

}

// execute the coroutine
/// Start(), Update():

// start coroutine
StartCoroutine(ExecuteDelayedOnce());


/* -----------------------------------------
   Execute Coroutine Looping
----------------------------------------- */
/// Class Body:
// define the coroutine

public Coroutine coroutine;

// coroutine that executes looping until you manually stop it with StopCoroutine()
IEnumerator ExecuteDelayedLooping()
{
    while(true) {

        yield return new WaitForSeconds(2.0f);
        // execute some code after 2 seconds here looping ...

    }
}

// execute the coroutine
/// Start(), Update():
coroutine = StartCoroutine(ExecuteDelayedLooping());  // keep a reference to the coroutine so you can stop it

// stop the coroutine when you need to, by using the stored reference
StopCoroutine(coroutine);
// or stop all coroutines on the object
StopAllCoroutines();

