using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public int maxRipples;
    public float playfieldWidth;
    public float playfieldHeight;
    public List<SoundRipple> soundRipples;
    public Text selectionText;

    public int currentRippleIndex;
    private SoundRipple currentRipple;

    public delegate void ChangeShapeEvent(int index);
    public static event ChangeShapeEvent OnShapeChange;

    private void Start()
    {
        currentRipple = soundRipples[currentRippleIndex];
    }

    public void Place(Vector2 pos, float freqMag, float heldTime)
    {
        if (!GameManager.gameStart)
            GameManager.GameStart();

        if (RippleManager.ripples.Count < RippleManager.MaxRipples)
        {

            Vector3 finalPos = new Vector3(pos.x, pos.y, 1f);
            Vector3 mousePoint = cam.ScreenToWorldPoint(finalPos);
            if (mousePoint.x < playfieldWidth && mousePoint.x > -playfieldWidth && mousePoint.y < playfieldHeight && mousePoint.y > -playfieldHeight)
            {
                SoundRipple clone = Instantiate(currentRipple);
                clone.transform.position = cam.ScreenToWorldPoint(finalPos);
                clone.Release(freqMag, heldTime, 1);
            }
        }
    }

    public void ChangeShape(int direction)
    {
        currentRippleIndex += direction;
        if(currentRippleIndex >= soundRipples.Count)
            currentRippleIndex = 0;
        if (currentRippleIndex < 0)
            currentRippleIndex = soundRipples.Count - 1;

        currentRipple = soundRipples[currentRippleIndex];
        selectionText.text = currentRipple.circleSteps.ToString();

        OnShapeChange.Invoke(currentRippleIndex);
    }
}
