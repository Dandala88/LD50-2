using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : Interactive
{
    public SoundRipple soundRipple;
    public float width;
    public float changeFrequency;
    public override void Interact(SoundRipple ripple)
    {
        SoundRipple clone1 = Instantiate(soundRipple);
        clone1.transform.position = transform.position + new Vector3(-width, 0f);
        clone1.Release(ripple.freqMag, ripple.heldTime);

        SoundRipple clone2 = Instantiate(soundRipple);
        clone2.transform.position = transform.position + new Vector3(width, 0f); ;
        clone2.Release(ripple.freqMag, ripple.heldTime, changeFrequency);

        Destroy(gameObject);
    }
}
