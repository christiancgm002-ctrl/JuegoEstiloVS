using UnityEngine;

public class HealthBar2D : MonoBehaviour
{
    [SerializeField] private Component target; // Player o Enemy (cualquiera que implemente IHealth)
    [SerializeField] private Transform fill;
    [SerializeField] private bool follow = true;
    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);

    private IHealth ih;

    void OnEnable()
    {
        ih = target as IHealth ?? (target ? target.GetComponent<IHealth>() : null);
        if (ih == null || !fill) { Debug.LogWarning("HealthBar2D: faltan refs o target no es IHealth"); return; }

        ih.AddHealthChangedListener(UpdateBar);
        UpdateBar(ih.CurrentHP, ih.MaxHP);
    }

    void OnDisable()
    {
        ih?.RemoveHealthChangedListener(UpdateBar);
    }

    void LateUpdate()
    {
        if (follow && target is Component c && c) transform.position = c.transform.position + offset;
    }

    void UpdateBar(int hp, int max)
    {
        float pct = (max > 0) ? (float)hp / max : 0f;
        fill.localScale = new Vector3(pct, 1f, 1f);
    }
}
