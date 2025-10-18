using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public Health health;
    public SpriteRenderer sprite;
    public float flashTime = 0.08f;

    void Awake()
    {
        if (!health) health = GetComponent<Health>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (health)
            health.onHealthChanged.AddListener(OnDamaged);
    }

    void OnDisable()
    {
        if (health)
            health.onHealthChanged.RemoveListener(OnDamaged);
    }

    void OnDamaged(int hp, int max)
    {
        // Si quieres flashear solo cuando baja la vida, podrías comprobarlo con un valor anterior.
        if (sprite) StartCoroutine(DoFlash());
    }

    System.Collections.IEnumerator DoFlash()
    {
        Color c = sprite.color;
        sprite.color = new Color(c.r, c.g, c.b, 0.4f);
        yield return new WaitForSeconds(flashTime);
        sprite.color = c;
    }
}
