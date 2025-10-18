// Scripts/UI/HealthToSlider.cs
using UnityEngine;
using UnityEngine.UI;

public class HealthToSlider : MonoBehaviour
{
    public Health health;   // arrastra aquí el Health del Player
    public Slider slider;   // este mismo Slider (o déjalo vacío y lo busca)

    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;

        if (health)
        {
            slider.value = health.Normalized();
            health.onHealthChanged.AddListener(OnHealthChanged);
        }
    }

    void OnDestroy()
    {
        if (health) health.onHealthChanged.RemoveListener(OnHealthChanged);
    }

    void OnHealthChanged(int hp, int max)
    {
        slider.value = max > 0 ? (float)hp / max : 0f;
    }
}