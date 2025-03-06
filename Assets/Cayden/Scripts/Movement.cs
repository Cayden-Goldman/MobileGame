using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int movementSpeed;
    float horizontalMovement;
    public int jumpForce;
    void Start()
    {

    }

    void Update()
    {
        rb2d.linearVelocityX = horizontalMovement * movementSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            rb2d.AddForceY(jumpForce);
        
    }
}
