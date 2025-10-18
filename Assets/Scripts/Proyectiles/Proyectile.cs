using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    int damage;
    float life;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Llama a esto justo tras instanciar
    public void Init(Vector2 dir, float speed, int dmg, float lifetime)
    {
        damage = dmg;
        life = lifetime;
        rb.linearVelocity = dir.normalized * speed;
        transform.up = dir; // orientativo
    }

    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>() ?? other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
