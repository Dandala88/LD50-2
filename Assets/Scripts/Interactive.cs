using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private void Awake()
    {
        RippleManager.interactives.Add(this);
    }

    virtual public void Interact(SoundRipple ripple)
    {
        
    }

    public void Move(Vector3 direction, float speed, float lifespan)
    {
        StartCoroutine(MoveCoroutine(direction, speed, lifespan));
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
}
