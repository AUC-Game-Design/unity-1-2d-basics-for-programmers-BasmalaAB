using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rigidbody2d;

    public float changeTime = 3.0f;
    float timer = 0;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
            timer = changeTime;
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
  }
}
