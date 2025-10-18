// Scripts/Player/HitFlash.cs
using UnityEngine;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    public Health health;
    public SpriteRenderer sr;
    public Color flashColor = new Color(1f, 0.5f, 0.5f);
    public float flashTime = 0.1f;

    Color _original;
    void Awake()
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();
        if (!health) health = GetComponent<Health>();
        if (sr) _original = sr.color;
        if (health) health.onHealthChanged.AddListener(OnChanged);
    }
    void OnDestroy()
    {
        if (health) health.onHealthChanged.RemoveListener(OnChanged);
    }
    void OnChanged(int hp, int max)
    {
        if (sr) StartCoroutine(Flash());
    }
    IEnumerator Flash()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        sr.color = _original;
    }
}