using System;
using Unity.Netcode;
using UnityEngine;

public class NetTransform : NetworkBehaviour
{
    Rigidbody2D playerBody;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (HasAuthority)
        {
            float theta = Time.frameCount / 10.0f;
            playerBody.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
        }
    }
}