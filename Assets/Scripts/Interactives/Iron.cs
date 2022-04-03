using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Interactive
{
    public float percentToDestroy; 
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            base.Interact(ripple, shape);
            int numToDestroy = Mathf.RoundToInt(RippleManager.ripples.Count * percentToDestroy);
            for (int i = 0; i < numToDestroy; i++)
            {
                Destroy(RippleManager.ripples[i].gameObject);
            }
            SoundRipple clone = Instantiate(Replicate(shape));
            clone.transform.SetParent(transform.parent);
            clone.transform.position = transform.position;
            clone.noise = true;
            clone.noiseFrequency = 1;
            clone.Release(ripple.freqMag, ripple.heldTime);
        }
        Destroy(gameObject);
    }
}
