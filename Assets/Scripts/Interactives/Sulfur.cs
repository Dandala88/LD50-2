using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sulfur : Interactive
{
    public int noiseFrequency;
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            base.Interact(ripple, shape);
            SoundRipple clone = Instantiate(Replicate(shape));
            clone.transform.position = transform.position;
            clone.noise = true;
            clone.noiseFrequency = noiseFrequency;
            clone.Release(ripple.freqMag, ripple.heldTime);
            StartCoroutine(CooldownCoroutine());
        }
    }
}
