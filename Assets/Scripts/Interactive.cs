using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private Coroutine moveCoroutine;
    public List<SoundRipple> soundRipples;
    public float cooldown;
    public int increaseMaxRipplesBy;

    protected bool cooled = true;
    protected Vector3 moveDirection;
    protected bool alreadyIncreased;

    private void Awake()
    {
        RippleManager.interactives.Add(this);
    }

    virtual public void Interact(SoundRipple ripple, SoundRipple.Shape shape)
    {
        switch(shape)
        {
            case SoundRipple.Shape.Circle:
                break;
            case SoundRipple.Shape.Square:
            break;
                case SoundRipple.Shape.Triangle:
            break;
            case SoundRipple.Shape.Pentagon:
                break;
        }
        if (!alreadyIncreased)
            RippleManager.maxRipples += increaseMaxRipplesBy;
        else
            alreadyIncreased = true;
    }

    public void Move(Vector3 direction, float speed, float lifespan)
    {
        moveDirection = direction;
        moveCoroutine = StartCoroutine(MoveCoroutine(direction, speed, lifespan));
    }

    private IEnumerator MoveCoroutine(Vector3 direction, float speed, float lifespan)
    {
        float currentTime = 0;
        while(currentTime < lifespan)
        {
            currentTime += Time.deltaTime;
            transform.position = transform.position + direction * speed * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        RippleManager.interactives.Remove(this);
    }

    public SoundRipple Replicate(SoundRipple.Shape shape)
    {
        switch (shape)
        {
            case SoundRipple.Shape.Circle:
                return soundRipples[0];
            case SoundRipple.Shape.Pentagon:
                return soundRipples[1];
            case SoundRipple.Shape.Square:
                return soundRipples[2];
            case SoundRipple.Shape.Triangle:
                return soundRipples[3];
            default:
                return soundRipples[0];
        }
    }

    protected IEnumerator CooldownCoroutine()
    {
        cooled = false;
        yield return new WaitForSeconds(cooldown);
        cooled = true;
    }
}
