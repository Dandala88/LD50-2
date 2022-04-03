using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRipple : MonoBehaviour
{
    public int sampleFreq = 44000;
    public float frequency = 440;
    public int seconds;
    public float curveFactor;
    public float volumeCurve;
    public float magnitudeFactor;
    public float minDelay;
    public float maxDelay;
    public float delay;
    public float decay;
    public float dry;
    public float wet;
    public float growthSpeed;
    public int circleSteps = 100;
    public float rotationSpeed;

    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float freqMag;
    [HideInInspector]
    public float heldTime;

    AudioSource aud;
    LineRenderer circle;
    float baseFrequency;
    private float rotateBy;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        circle = GetComponentInChildren<LineRenderer>();
        baseFrequency = frequency;
        RippleManager.ripples.Add(this);
        circle.colorGradient = RandomColorGenerator();
    }

    public void Update()
    {
        radius += growthSpeed * Time.deltaTime;
        DrawCircle(transform.position, circleSteps, radius);
        aud.volume -= Time.deltaTime / (seconds * volumeCurve);
        foreach(Interactive interactive in RippleManager.interactives)
        {
            if(Vector2.Distance(transform.position, interactive.transform.position) < radius)
            {
                interactive.Interact(this);
            }
        }
    }

    public void Release(float magnitude, float holdingTime, float newFrequency)
    {
        frequency = newFrequency;
        Release(magnitude, holdingTime);
    }

    public void Release(float magnitude, float holdingTime)
    {
        freqMag = magnitude;
        heldTime = holdingTime;
        growthSpeed = Mathf.Abs(magnitude * magnitudeFactor);
        if (magnitude == 1)
            frequency = baseFrequency;
        else
            frequency = baseFrequency * magnitude * magnitudeFactor;
        delay = Mathf.Clamp(delay / holdingTime, minDelay, maxDelay);
        CreateSound();
    }


    public void CreateSound()
    {

        StartCoroutine(WaitForDeathCoroutine());
        int finalSamples = sampleFreq * seconds;

        float[] samples = new float[finalSamples];
        for (int i = 0; i < samples.Length; i++)
        {
            frequency -= (frequency / sampleFreq / seconds) * curveFactor;
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

    private IEnumerator WaitForDeathCoroutine()
    {yield return new WaitForSeconds(seconds);
        RippleManager.ripples.Remove(this);
        Destroy(gameObject);
    }

    public void DrawCircle(Vector3 center, int steps, float radius)
    {
        circle.positionCount = steps;

        rotateBy += rotationSpeed * Time.deltaTime;
        for (int i = 0; i < steps; i++)
        {
            float circumferenceProgress = (float)i / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI + rotateBy;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);
            float x = xScaled * radius;
            float y = yScaled * radius;
            Vector3 currentPosition = new Vector3(x, y, 0) + center;
            circle.SetPosition(i, currentPosition);
        }
    }

    private Gradient RandomColorGenerator()
    {
        Gradient randGradient = new Gradient();
        GradientColorKey[] cKeys = new GradientColorKey[2];
        cKeys[0].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        cKeys[0].time = 0f;
        cKeys[1].color = cKeys[0].color;
        cKeys[1].time = 1f;
        GradientAlphaKey[] aKeys = new GradientAlphaKey[2];
        aKeys[0].alpha = 1f;
        aKeys[0].time = 0;
        aKeys[1].alpha = 1f;
        aKeys[1].time = 1;
        randGradient.SetKeys(cKeys, aKeys);
        return randGradient;
    }

    private void OnDestroy()
    {
        RippleManager.ripples.Remove(this);
    }
}
