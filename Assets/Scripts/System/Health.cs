using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Vida")]
    public int maxHP = 5;
    [SerializeField] int currentHP;

    [Header("I-Frames")]
    public float invincibleTime = 0.2f;
    float iframesUntil;

    [Header("Eventos")]
    public UnityEvent<int, int> OnHealthChanged; // (hpActual, hpMax)
    public UnityEvent OnDamaged;
    public UnityEvent OnDeath;

    void OnEnable()
    {
        currentHP = Mathf.Max(1, maxHP);
        iframesUntil = 0f;
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public bool IsAlive => currentHP > 0;

    public void Heal(int amount)
    {
        if (!IsAlive) return;
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        if (Time.time < iframesUntil) return; // invencible

        currentHP -= Mathf.Max(1, amount);
        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke();
        }
        else
        {
            iframesUntil = Time.time + invincibleTime;
        }
    }
}