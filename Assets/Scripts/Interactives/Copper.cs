using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copper : Interactive
{
    public int seconds;
    public float frequencyMultiplier;
    public float growthSpeedMultiplier;
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            shape = SoundRipple.GetShapeFromInt((int)shape + 1);
            base.Interact(ripple, shape);
            SoundRipple clone = Instantiate(Replicate(shape));
            clone.transform.position = transform.position;
            clone.seconds = seconds;
            clone.frequency *= frequencyMultiplier;
            clone.growthSpeed *= growthSpeedMultiplier;
            clone.Release(ripple.freqMag, ripple.heldTime);
            StartCoroutine(CooldownCoroutine());
        }
    }
}
