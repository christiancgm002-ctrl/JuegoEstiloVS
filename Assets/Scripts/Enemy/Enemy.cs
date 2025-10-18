using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 1;   // 🔹 visible en el Inspector
    public Health health;

    private Transform target;

    void Awake()
    {
        if (!health) health = GetComponent<Health>();
        if (health != null)
            health.onDeath.AddListener(Die);
    }

    // Ya no hace falta pasar daño aquí
    public void Init(Transform t)
    {
        target = t;

        if (!health) health = GetComponent<Health>();

        if (health != null)
        {
            health.currentHP = health.maxHP;
            health.onHealthChanged.Invoke(health.currentHP, health.maxHP);
        }

        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!target) return;
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        if (!health) health = GetComponent<Health>();
        health?.TakeDamage(dmg);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>() ?? other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage); // 🔹 usa su propio daño interno
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
