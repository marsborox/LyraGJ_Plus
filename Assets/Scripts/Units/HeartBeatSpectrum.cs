using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    public float bpm = 120f;
    public float pulseStrength = 0.2f; // how much it grows
    private float baseScale = 1f;

    void Start()
    {
        baseScale = transform.localScale.x;
    }

    void Update()
    {
        float beatInterval = 60f / bpm;
        float time = Time.time % beatInterval;

        float t = Mathf.Sin((time / beatInterval) * Mathf.PI);
        float scale = baseScale + t * pulseStrength;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}

