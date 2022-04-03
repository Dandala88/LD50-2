using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tin : Interactive
{
    public int noiseFrequency;
    public float curveFactor;
    public int maxNoiseFactor;
    public float tinCooldown;
    public override void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        if (cooled && RippleManager.ripples.Count < RippleManager.MaxRipples)
        {
            base.Interact(ripple, shape);
            StartCoroutine(TinSequence(ripple, shape, ripple.transform.position));
            StartCoroutine(CooldownCoroutine());
        }
        Destroy(ripple.gameObject);
    }

    private IEnumerator TinSequence(SoundRipple ripple, SoundRipple.Shape shape, Vector3 position)
    {
        TinReplicate(ripple, shape, position - moveDirection);
        yield return new WaitForSeconds(tinCooldown);
        TinReplicate(ripple, shape, position - moveDirection * 1.5f);
        yield return new WaitForSeconds(tinCooldown);
        TinReplicate(ripple, shape, position - moveDirection * 2);
    }
    private void TinReplicate(SoundRipple ripple, SoundRipple.Shape shape, Vector3 position)
    {
        SoundRipple clone = Instantiate(Replicate(shape));
        clone.transform.position = position;
        clone.transform.SetParent(transform.parent);
        clone.noise = true;
        clone.noiseFrequency = noiseFrequency;
        clone.maxNoiseFactor = maxNoiseFactor;
        clone.curveFactor = curveFactor;
        clone.Release(ripple.freqMag, ripple.heldTime);
    }
}
