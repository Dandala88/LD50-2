using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundRipple soundRipple;
    public int maxRipples;
    public void Place(Vector2 pos, float freqMag, float heldTime)
    {
        if (RippleManager.ripples.Count < maxRipples)
        {
            SoundRipple clone = Instantiate(soundRipple);
            Vector3 finalPos = new Vector3(pos.x, pos.y, 1f);
            clone.transform.position = Camera.main.ScreenToWorldPoint(finalPos);
            clone.Release(freqMag, heldTime);
        }
    }
}
