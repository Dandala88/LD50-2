using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public PlayerController player;
    public Vector2 mousePos;
    public float rollMagnitudeThreshold;
    public float rollCooldown;

    [HideInInspector]
    public SoundRipple currentRipple;

    private bool holdingPlace;
    private bool coolingDown;
    private float holdingTime;

    private void Update()
    {
        if(holdingPlace)
            holdingTime += Time.deltaTime;
    }

    public void Place(CallbackContext context)
    {
        if(context.started)
        {
            holdingPlace = true;
        }

        if(context.canceled)
        {
            holdingPlace = false;
            player.Place(mousePos, 1, holdingTime);
            holdingTime = 0;
        }
    }

    public void Move(CallbackContext context)
    {
        if(context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }

    public void Roll(CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 mouseMove = context.ReadValue<Vector2>();
            if (mouseMove.magnitude > rollMagnitudeThreshold)
            {
                if (holdingPlace && !coolingDown)
                {
                    player.Place(mousePos, mouseMove.y, mouseMove.x);
                    StartCoroutine(RollCooldown());
                }
            }
        }
    }

    private IEnumerator RollCooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(rollCooldown);
        coolingDown = false;
    }
}
