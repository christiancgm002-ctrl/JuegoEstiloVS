using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Health health;  // referencia al componente Health
    private Transform target;

    void Awake()
    {
        // obtiene el componente Health en el mismo GameObject
        if (!health) health = GetComponent<Health>();
        // cuando la vida llega a 0, destruye el enemigo
        if (health != null)
            health.onDeath.AddListener(Die);
    }

    public void Init(Transform t)
    {
        target = t;

        if (!health) health = GetComponent<Health>();

        if (health != null)
        {
            health.currentHP = health.maxHP; // reinicia vida
            // fuerza actualización de la barra
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
        health?.TakeDamage(dmg);  // <-- aquí usa el componente Health
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
