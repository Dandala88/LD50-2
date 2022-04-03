using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Interactive
{
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        base.Interact(ripple, shape);
        Destroy(ripple.gameObject);
    }
}
