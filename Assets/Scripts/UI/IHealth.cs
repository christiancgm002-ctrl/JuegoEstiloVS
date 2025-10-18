using UnityEngine.Events;

public interface IHealth
{
    int MaxHP { get; }
    int CurrentHP { get; }

    void AddHealthChangedListener(UnityAction<int, int> listener);
    void RemoveHealthChangedListener(UnityAction<int, int> listener);

    void AddDeathListener(UnityAction listener);
    void RemoveDeathListener(UnityAction listener);

    void TakeDamage(int dmg);
    void Heal(int amount);
}