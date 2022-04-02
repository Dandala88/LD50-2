using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public PlayerController player;
    public Vector2 mousePos;

    public void Place(CallbackContext context)
    {
        if(context.started)
        {
            player.Place(mousePos);
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
