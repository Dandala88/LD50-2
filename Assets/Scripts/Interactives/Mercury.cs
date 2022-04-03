using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mercury : Interactive
{
    public int secondsMultiplier;
    public float frequencyMultiplier;
    public float growthSpeedMultiplier;
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.maxRipples)
        {
            base.Interact(ripple, shape);
            SoundRipple clone = Instantiate(Replicate(shape));
            clone.transform.position = transform.position;
            clone.seconds *= secondsMultiplier;
            clone.frequency *= frequencyMultiplier;
            clone.growthSpeed *= growthSpeedMultiplier;
            clone.Release(ripple.freqMag, ripple.heldTime);
            StartCoroutine(CooldownCoroutine());
        }
    }
}
