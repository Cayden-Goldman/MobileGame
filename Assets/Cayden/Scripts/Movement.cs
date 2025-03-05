using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    float horizontalMovement;
    void Start()
    {

    }

    void Update()
    {
        rb2d.linearVelocity = new Vector2(horizontalMovement, rb2d.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
}
