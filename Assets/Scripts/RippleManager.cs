using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleManager : MonoBehaviour
{
    public static int numRipples = 7;
    private static int maxRipples = 33;

    public static int MaxRipples
    {
        get { return numRipples; }
        set 
        { 
            if(value < maxRipples)
                numRipples = value; 
        }
    }

    public static List<SoundRipple> ripples = new List<SoundRipple>();

    public static List<Vector3> vertices = new List<Vector3>();

    public static List<Interactive> interactives = new List<Interactive>();
}

