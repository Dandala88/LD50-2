using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Interactive
{
    public float curveFactor;
    public float goldCooldown;
    public int sections;
    public float radius;
    public float frequencyChange;
    public float holdtimeChange;
    public int seconds;

    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            base.Interact(ripple, shape);
            StartCoroutine(GoldSequence(ripple, shape, transform.position, sections));
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator GoldSequence(SoundRipple ripple, SoundRipple.Shape shape, Vector3 position, int sections)
    {
        float angle = 360 / sections;
        
        for(int i = 0; i < sections; i++)
        {
            GoldReplicate(ripple, SoundRipple.GetShapeFromInt(i), position + GetPostionFromSection(radius, i, angle));
            yield return new WaitForSeconds(goldCooldown);
        }
    }

    private void GoldReplicate(SoundRipple ripple, SoundRipple.Shape shape, Vector3 position)
    {
        SoundRipple clone = Instantiate(Replicate(shape));
        clone.transform.position = position;
        clone.curveFactor = curveFactor;
        clone.seconds = seconds;
        clone.Release(ripple.freqMag, ripple.heldTime * holdtimeChange, frequencyChange);
    }

    private Vector3 GetPostionFromSection(float radius, int section, float angle)
    {
        float x = Mathf.Cos(angle * section * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * section * Mathf.Deg2Rad) * radius;
        return new Vector3 (x, y);
    }
}
