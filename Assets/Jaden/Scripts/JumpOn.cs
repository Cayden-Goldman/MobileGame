using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class JumpOn : NetworkBehaviour
{
    public float jumpOnVelocity = 10;

    bool destroying;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOwner) return;

        if (collision.CompareTag("Enemy") && !destroying)
        {
            NetworkObject netObject = collision.gameObject.GetComponent<NetworkObject>();

            if (netObject)
                StartCoroutine(DestroyWait(netObject));
        }
    }

    private IEnumerator DestroyWait(NetworkObject enemy) // network object serializes to a reference
    {
        destroying = true;
        yield return new WaitForSeconds(.1f);
        
        rb.linearVelocityY = jumpOnVelocity;
        HitEnemyRpc(enemy, NetworkObjectId);

        yield return new WaitForSeconds(.25f);
        destroying = false;
    }

    // Network
    [Rpc(SendTo.Server)]
    void HitEnemyRpc(NetworkObjectReference enemyReference, ulong sourceNetworkObjectId)
    {
        NetworkObject enemy = enemyReference;

        if (enemy != null)
            Destroy(enemy.gameObject); // only destroys on server for some reason
    }
}
