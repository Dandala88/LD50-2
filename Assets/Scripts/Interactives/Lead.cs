using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lead : Interactive
{
    public int seconds;
    public float frequencyMultiplier;
    public float growthSpeedMultiplier;
    public float curveMultiplier;
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            base.Interact(ripple, shape);
            SoundRipple clone = Instantiate(Replicate(shape));
            clone.transform.position = transform.position - moveDirection;
            clone.seconds = seconds;
            clone.frequency *= frequencyMultiplier;
            clone.growthSpeed *= growthSpeedMultiplier;
            clone.curveFactor *= curveMultiplier;
            clone.Release(ripple.freqMag, ripple.heldTime);
            StartCoroutine(CooldownCoroutine());
        }

        Destroy(ripple.gameObject);
    }
}
