using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundRipple soundRipple;
    public int maxRipples;
    public float playfieldWidth;
    public float playfieldHeight;
    public int baseFrequency;

    public void Place(Vector2 pos, float freqMag, float heldTime)
    {
        if (RippleManager.ripples.Count < maxRipples)
        {

            Vector3 finalPos = new Vector3(pos.x, pos.y, 1f);
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(finalPos);
            if (mousePoint.x < playfieldWidth && mousePoint.x > -playfieldWidth && mousePoint.y < playfieldHeight && mousePoint.y > -playfieldHeight)
            {
                SoundRipple clone = Instantiate(soundRipple);
                clone.transform.position = Camera.main.ScreenToWorldPoint(finalPos);
                clone.Release(freqMag, heldTime, baseFrequency);
            }
        }
    }
}
