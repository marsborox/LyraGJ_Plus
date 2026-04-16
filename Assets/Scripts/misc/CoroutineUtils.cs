using System.Collections;
using UnityEngine;

public static class CoroutineUtils
{
    /// <summary>
    /// Waits for the given duration in real-time, unaffected by Time.timeScale.
    /// </summary>
    public static IEnumerator WaitForSecondsRealtime(float duration)
    {
        float endTime = Time.realtimeSinceStartup + duration;
        while (Time.realtimeSinceStartup < endTime)
        {
            yield return null; // wait one frame, then check again
        }
    }
}