using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public PlayerController player;
    public Vector2 mousePos;
    public SoundRipple currentRipple;

    private bool holdingPlace;
    private float placeHoldTime;

    public void Update()
    {
        if (holdingPlace)
            placeHoldTime += Time.deltaTime;
    }

    public void Place(CallbackContext context)
    {
        if(context.started)
        {
            holdingPlace = true;
            placeHoldTime = 0;
            currentRipple = player.Place(mousePos);
        }

        if(context.canceled)
        {
            holdingPlace = false;
            currentRipple.Release();
        }
    }

    public void Move(CallbackContext context)
    {
        if(context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }


}
