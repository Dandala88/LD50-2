using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxRipples;
    public float playfieldWidth;
    public float playfieldHeight;
    public List<SoundRipple> soundRipples;

    private int currentRippleIndex;
    private SoundRipple currentRipple;

    private void Start()
    {
        currentRipple = soundRipples[currentRippleIndex];
    }

    public void Place(Vector2 pos, float freqMag, float heldTime)
    {
        if (RippleManager.ripples.Count < maxRipples)
        {

            Vector3 finalPos = new Vector3(pos.x, pos.y, 1f);
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(finalPos);
            if (mousePoint.x < playfieldWidth && mousePoint.x > -playfieldWidth && mousePoint.y < playfieldHeight && mousePoint.y > -playfieldHeight)
            {
                SoundRipple clone = Instantiate(currentRipple);
                clone.transform.position = Camera.main.ScreenToWorldPoint(finalPos);
                clone.Release(freqMag, heldTime, 1);
            }
        }
    }

    public void ChangeShape(int direction)
    {
        currentRippleIndex = Mathf.Clamp(currentRippleIndex + direction, 0, soundRipples.Count - 1);
        currentRipple = soundRipples[currentRippleIndex];
    }
}
