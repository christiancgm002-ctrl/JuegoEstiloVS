using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health targetHealth;
    public Slider slider;

    void Awake()
    {
        if (slider != null)
        {
            slider.minValue = 0;
        }
    }

    void OnEnable()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged.AddListener(UpdateBar);
            // inicializa con los valores actuales
            UpdateBar(targetHealthIsNull ? 0 : targetHealthMax(), targetHealthMax());
        }
    }

    void OnDisable()
    {
        if (targetHealth != null)
            targetHealth.OnHealthChanged.RemoveListener(UpdateBar);
    }

    void UpdateBar(int hp, int max)
    {
        if (slider == null) return;
        slider.maxValue = max;
        slider.value = hp;
    }

    int targetHealthMax() => targetHealth ? targetHealth.GetType()
        .GetField("maxHP").GetValue(targetHealth) as int? ?? 1 : 1;

    bool targetHealthIsNull => targetHealth == null;
}