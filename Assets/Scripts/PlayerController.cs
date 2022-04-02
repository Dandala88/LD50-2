using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundRipple soundRipple;
    public void Place()
    {
        SoundRipple clone = Instantiate(soundRipple);
    }
}
