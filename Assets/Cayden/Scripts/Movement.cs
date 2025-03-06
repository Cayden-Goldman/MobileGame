using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class Movement : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public int movementSpeed;
    float horizontalMovement;
    public int jumpForce;

    void Update()
    {
        if (IsOwner)
            rb2d.linearVelocityX = horizontalMovement * movementSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsOwner)
            rb2d.AddForceY(jumpForce);
    }
}
