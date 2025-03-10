using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class JumpOn : NetworkBehaviour
{
    public float jumpOnVelocity = 10;

    bool destroying;
    public Rigidbody2D rb;

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
            enemy.Despawn(true);
    }
}
