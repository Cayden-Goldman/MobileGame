using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class funnypunch : NetworkBehaviour
{
    BoxCollider2D hitbox;
    SpriteRenderer sprite;

    [Header("Constants")]
    public float punchDuration = 0.4f;
    public float punchCooldown = 0.75f;

    [Header("State")]
    public bool isActive = false;
    public bool onCooldown = false;
    List<NetworkObject> hit = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Input
    public void PunchInputAction(InputAction.CallbackContext context)
    {
        if (context.performed)
            Punch();
    }

    // Action

    public bool canPunch()
    {
        return IsOwner && !isActive && !onCooldown;
    }

    public void Punch()
    {
        if (!canPunch()) return;

        hit.Clear();

        isActive = true;
        hitbox.enabled = true;
        sprite.enabled = true;

        StartCoroutine(punchTime());
    }

    public void StopPunch()
    {
        if (!isActive) return;

        onCooldown = true;

        isActive = false;
        hitbox.enabled = false;
        sprite.enabled = false;

        StartCoroutine(cooldown());
    }

    IEnumerator punchTime()
    {
        yield return new WaitForSeconds(punchDuration);
        StopPunch();
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(punchCooldown);
        onCooldown = false;
    }

    // Hit Detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkObject netObject = collision.gameObject.GetComponent<NetworkObject>();

        if (netObject != null)
        {
            if (hit.Contains(netObject)) return;

            hit.Add(netObject);
            PunchHitRpc(netObject, NetworkObjectId);
        }
            
    }

    [Rpc(SendTo.Server)]
    void PunchHitRpc(NetworkObjectReference objectReference, ulong sourceNetworkObjectId)
    {
        NetworkObject character = objectReference;

        if (!character)
            return;

        Rigidbody2D netBody = character.GetComponent<Rigidbody2D>();

        if (!netBody)
            return;

        netBody.AddForceX(7000); // todo: add like a proper force
    }
}
