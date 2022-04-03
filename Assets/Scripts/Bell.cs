using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : Interactive
{
    public SoundRipple soundRipple;

    

    public override void Interact(SoundRipple ripple)
    {
        SoundRipple clone = Instantiate(soundRipple);
        clone.transform.position = transform.position;
        clone.Release(ripple.freqMag, ripple.heldTime);
        base.Interact(ripple);
        Destroy(gameObject);
    }
}
