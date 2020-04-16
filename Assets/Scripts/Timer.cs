using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * This class takes care of calling a method
 * every X seconds.
 * 
 * Big thanks to Indie Nuggets for the code
 * https://github.com/indienuggets/INUtils/blob/master/Timer.cs
*/
public class Timer : MonoBehaviour
{
    public float runEvery = 5;
    public bool runOnAwake = false;
    public bool loop = true;

    // The method to call.
    public UnityEvent onTimerEnd;

    void OnEnable()
    {
        // Invoke the method on awake if desired.
        if (runOnAwake)
        {
            Invoke("Execute", 0);
        }

        // Invoke the method every X seconds
        if (loop)
        {
            InvokeRepeating("Execute", runEvery, runEvery);
        }
        else
        {
            // Run it once.
            Invoke("Execute", runEvery);
        }
    }

    public void Execute()
    {
        onTimerEnd.Invoke();
    }
}
