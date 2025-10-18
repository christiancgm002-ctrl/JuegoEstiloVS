using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1f; // disparos por segundo
    public float projectileSpeed = 10f;
    public int damage = 1;
    public float lifetime = 2f;
    float cooldown;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            Fire();
            cooldown = 1f / fireRate;
        }
    }

    void Fire()
    {
        // busca un enemigo cercano (opcional)
        var target = FindClosestEnemy();
        Vector2 dir = target ?
            (target.position - transform.position).normalized :
            Vector2.right; // si no hay enemigos, dispara a la derecha

        var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().Init(dir, projectileSpeed, damage, lifetime);
    }

    Transform FindClosestEnemy()
    {
        var enemies = FindObjectsOfType<Enemy>();
        Transform closest = null;
        float minDist = float.MaxValue;
        foreach (var e in enemies)
        {
            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = e.transform;
            }
        }
        return closest;
    }
}