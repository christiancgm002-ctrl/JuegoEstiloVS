// Scripts/Enemy/EnemyContactDamage.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyContactDamage : MonoBehaviour
{
    public int damage = 1;
    public float tickInterval = 0.5f; // cada cuánto aplicar daño al estar en contacto

    float timer;

    void Update()
    {
        if (timer > 0f) timer -= Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        TryDamage(col.collider);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // Por si algún enemigo usa trigger en el futuro
        TryDamage(col);
    }

    void TryDamage(Collider2D col)
    {
        if (timer > 0f) return;

        var health = col.GetComponent<Health>() ?? col.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            timer = tickInterval;
        }
    }
}