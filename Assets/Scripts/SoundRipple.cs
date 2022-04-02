using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRipple : MonoBehaviour
{
    public int sampleFreq = 44000;
    public float frequency = 880;
    public int seconds;
    public float curveFactor;
    public float delay;
    public float decay;
    public float dry;
    public float wet;
    public float growthSpeed;
    public int circleSteps = 100;

    [HideInInspector]
    public float radius;

    AudioSource aud;
    LineRenderer circle;
    CircleCollider2D circleCollider;
    bool released;
    float holdingTime;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        circle = GetComponentInChildren<LineRenderer>();
    }

    public void Start()
    {
        circle.enabled = false;
        circleCollider.radius = radius;
    }

    public void Update()
    {
        if(!released)
        {
            holdingTime += Time.deltaTime;
        }
        else
        {
            circleCollider.radius = radius;
            radius += growthSpeed * Time.deltaTime;
            DrawCircle(transform.position, 100, radius);
        }
    }

    public void Release()
    {
        circle.enabled = true;
        released = true;
        delay /= holdingTime;
        CreateSound();
    }


    public void CreateSound()
    {
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
        StartCoroutine(WaitForDeathCoroutine());
    }

    private IEnumerator WaitForDeathCoroutine()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void DrawCircle(Vector3 center, int steps, float radius)
    {
        circle.positionCount = steps;

        for (int i = 0; i < steps; i++)
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
