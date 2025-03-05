using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class Movement : MonoBehaviour
{
    InputAction move;
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        
    }
}
