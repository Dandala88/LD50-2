using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private void Awake()
    {
        RippleManager.interactives.Add(this);
    }

    virtual public void Interact(SoundRipple ripple)
    {

    }

    private void OnDestroy()
    {
        RippleManager.interactives.Remove(this);
    }
}
