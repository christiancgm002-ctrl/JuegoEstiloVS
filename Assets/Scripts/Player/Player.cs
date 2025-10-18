using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthChangedEvent : UnityEvent<int, int> { } // (current, max)

public class Player : MonoBehaviour, IHealth
{
    [Header("Vida del Player")]
    [SerializeField] private int maxHP = 10;
    [SerializeField] private int currentHP = 0;

    [Header("Eventos")]
    [SerializeField] private HealthChangedEvent onHealthChanged = new HealthChangedEvent();
    [SerializeField] private UnityEvent onDeath = new UnityEvent();

    [Header("Daño")]
    [SerializeField] private float damageCooldown = 0.5f;
    private float lastDamageTime = -1f;

    // === IHealth ===
    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    public void AddHealthChangedListener(UnityAction<int, int> l) => onHealthChanged.AddListener(l);
    public void RemoveHealthChangedListener(UnityAction<int, int> l) => onHealthChanged.RemoveListener(l);
    public void AddDeathListener(UnityAction l) => onDeath.AddListener(l);
    public void RemoveDeathListener(UnityAction l) => onDeath.RemoveListener(l);
    void EmitHealthChanged() => onHealthChanged?.Invoke(currentHP, maxHP);
    void Awake()
    {
        if (currentHP <= 0) currentHP = maxHP;   // empieza lleno
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void Init()
    {
        currentHP = maxHP;                       // reinicia vida
        onHealthChanged?.Invoke(currentHP, maxHP);
        gameObject.SetActive(true);
    }

    public void TakeDamage(int dmg)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        lastDamageTime = Time.time;

        if (dmg <= 0) return;
        currentHP = Mathf.Max(0, currentHP - dmg);
        onHealthChanged?.Invoke(currentHP, maxHP);
        if (currentHP == 0) onDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    // (Opcional) cambiar max y rellenar
    public void SetMax(int newMax, bool refill = true)
    {
        var prev = currentHP;
        maxHP = Mathf.Max(1, newMax);
        if (refill) currentHP = maxHP; else currentHP = Mathf.Clamp(prev, 0, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}