using UnityEngine;
using UnityEngine.Events;



public class Health : MonoBehaviour, IHealth
{
    [Header("Config")]
    public int maxHP = 5;

    [Header("Runtime")]
    public int currentHP;

    [Header("Events")]
    public HealthChangedEvent onHealthChanged = new HealthChangedEvent();
    public UnityEvent onDeath = new UnityEvent();

    // === IHealth ===
    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;

    public void AddHealthChangedListener(UnityAction<int, int> l) => onHealthChanged.AddListener(l);
    public void RemoveHealthChangedListener(UnityAction<int, int> l) => onHealthChanged.RemoveListener(l);
    public void AddDeathListener(UnityAction l) => onDeath.AddListener(l);
    public void RemoveDeathListener(UnityAction l) => onDeath.RemoveListener(l);

    void Awake()
    {
        if (currentHP <= 0) currentHP = maxHP;
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(int dmg)
    {
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
}
