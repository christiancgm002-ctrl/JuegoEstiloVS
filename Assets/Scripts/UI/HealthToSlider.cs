//using UnityEngine;
//using UnityEngine.UI;

//public class HealthToSlider : MonoBehaviour
//{
//    public Health targetHealth;
//    public Slider slider;

//    void OnEnable()
//    {
//        if (targetHealth && slider)
//        {
//            slider.minValue = 0f;
//            slider.maxValue = 1f;
//            slider.value = targetHealth.Normalized();
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
//        slider.value = max > 0 ? (float)hp / max : 0f;
//    }
//}
