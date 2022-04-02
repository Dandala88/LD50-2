using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRipple : MonoBehaviour
{
    public int sampleFreq = 44000;
    public float frequency = 440;
    public int seconds;
    public float curveFactor;
    AudioSource aud;

    void Start()
    {
        int finalSamples = sampleFreq * seconds;

        float[] samples = new float[finalSamples];
        for (int i = 0; i < samples.Length; i++)
        {
            frequency -= frequency / sampleFreq / (seconds * curveFactor);
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq);
        }
        AudioClip ac = AudioClip.Create("Test", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);

        aud = GetComponent<AudioSource>();
        aud.clip = ac;
        aud.Play();
    }
}
