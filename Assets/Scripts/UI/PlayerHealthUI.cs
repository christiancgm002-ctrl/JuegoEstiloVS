//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerHealthUI : MonoBehaviour
//{
//    public Health targetHealth;
//    public Slider slider;

//    void Awake()
//    {
//        if (!targetHealth) targetHealth = FindFirstObjectByType<Health>();
//    }

//    void OnEnable()
//    {
//        if (targetHealth && slider)
//        {
//            slider.minValue = 0;
//            slider.maxValue = targetHealth.MaxHP;
//            slider.value = targetHealth.CurrentHP;

//            // ⬇️ nombre correcto del evento
//            targetHealth.onHealthChanged.AddListener(OnHealthChanged);
//        }
//    }

//    void OnDisable()
//    {
//        if (targetHealth)
//            targetHealth.onHealthChanged.RemoveListener(OnHealthChanged);
//    }

//    void OnHealthChanged(int hp, int max)
//    {
//        if (!slider) return;
//        if (slider.maxValue != max) slider.maxValue = max;
//        slider.value = hp;
//    }
//}
