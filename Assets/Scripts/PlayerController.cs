using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundRipple soundRipple;
    public SoundRipple Place(Vector2 pos)
    {
        SoundRipple clone = Instantiate(soundRipple);
        Vector3 finalPos = new Vector3(pos.x, pos.y, 1f);
        clone.transform.position = Camera.main.ScreenToWorldPoint(finalPos);
        return clone;
    }
}
