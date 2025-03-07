using System.Collections;
using UnityEngine;

public class JumpOn : MonoBehaviour
{
    public bool destroying;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !destroying)
        {
            StartCoroutine(DestroyWait(collision.gameObject));
        }
    }
    private IEnumerator DestroyWait(GameObject destroy)
    {
        destroying = true;
        yield return new WaitForSeconds(.1f);
        Destroy(destroy);
        rb.linearVelocityY = 5;
        destroying = false;
    }
}
