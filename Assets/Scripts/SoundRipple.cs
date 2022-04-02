using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRipple : MonoBehaviour
{
    public int sampleFreq = 44000;
    public float frequency = 440;
    public int seconds;
    public float curveFactor;
    public float delay;
    public float decay;
    public float dry;
    public float wet;
    public float growthSpeed;
    AudioSource aud;
    LineRenderer circle;
    float radius;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        circle = GetComponentInChildren<LineRenderer>();
    }

    public void Start()
    {
        CreateSound();
    }

    public void Update()
    {
        radius += growthSpeed * Time.deltaTime;
        DrawCircle(transform.position, 100, radius);
    }


    public void CreateSound()
    {
        int finalSamples = sampleFreq * seconds;

        float[] samples = new float[finalSamples];
        for (int i = 0; i < samples.Length; i++)
        {
            frequency -= frequency / sampleFreq / (seconds * curveFactor);
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq);
        }
        AudioClip ac = AudioClip.Create("RippleSound", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);

        aud.clip = ac;

        AudioEchoFilter filter = gameObject.AddComponent<AudioEchoFilter>();
        filter.delay = delay;
        filter.decayRatio = decay;
        filter.dryMix = dry;
        filter.wetMix = wet;
        aud.Play();
    }

    public void DrawCircle(Vector3 center, int steps, float radius)
    {
        circle.positionCount = steps;

        for(int i = 0; i < steps; i++)
        {
            float circumferenceProgress = (float)i / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);
            float x = xScaled * radius;
            float y = yScaled * radius;
            Vector3 currentPosition = new Vector3(x, y, 0) + center;
            circle.SetPosition(i, currentPosition);
        }
    }
}
