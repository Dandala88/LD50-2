using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Interactive
{
    public override void Interact(SoundRipple ripple)
    {
        Destroy(ripple.gameObject);
    }
}
