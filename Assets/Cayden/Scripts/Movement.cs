using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using Unity.Cinemachine;

public class Movement : NetworkBehaviour
{
    public Rigidbody2D rb2d;
    public int movementSpeed;
    float horizontalMovement;
    public int jumpForce;
    GameObject cam;

    private void Start()
    {
        if (IsOwner)
        {
            GetComponent<PlayerInput>().enabled = true;
            cam = GameObject.Find("CinemachineCamera");
            cam.GetComponent<CinemachineCamera>().Follow = transform;
        }
    }
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
