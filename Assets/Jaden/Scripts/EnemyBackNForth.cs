using UnityEngine;

public class EnemyBackNForth : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2.linearVelocityX = speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
            speed = -speed;
    }
}
