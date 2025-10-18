using UnityEngine;

public class HealthBar2D : MonoBehaviour
{
    public Health health;            // si está vacío, se busca en el padre
    public Transform fillTransform;  // HB_Fill
    public float showSecondsOnChange = 1f;
    public bool hideWhenFull = true;

    SpriteRenderer fillSR;
    bool useSize;          // true si el fill usa DrawMode != Simple
    Vector3 baseScale;
    Vector2 baseSize;
    float showTimer;

    void Awake()
    {
        if (!health) health = GetComponentInParent<Health>();


        fillSR = fillTransform.GetComponent<SpriteRenderer>();
        if (fillSR != null && fillSR.drawMode != SpriteDrawMode.Simple)
        {
            useSize = true;
            baseSize = fillSR.size;
        }
        else
        {
            useSize = false;
            baseScale = fillTransform.localScale;
        }

        if (hideWhenFull) gameObject.SetActive(false);
    }

    void OnEnable()
    {

        if (health != null) health.onHealthChanged.AddListener(OnHealthChanged);
        // refresco inicial
        if (health != null) OnHealthChanged(health.currentHP, health.maxHP);
    }

    void OnDisable()
    {
        if (health != null) health.onHealthChanged.RemoveListener(OnHealthChanged);
    }

    void Update()
    {
        if (showTimer > 0f)
        {
            showTimer -= Time.deltaTime;
            if (showTimer <= 0f && hideWhenFull && Mathf.Approximately(health.Normalized(), 1f))
                gameObject.SetActive(false);
        }
    }

    void OnHealthChanged(int hp, int max)
    {
        // DEBUG: confirma que llega el evento
        
        float t = max > 0 ? (float)hp / max : 0f;
        t = Mathf.Clamp01(t);

        if (useSize)
        {
            fillSR.size = new Vector2(baseSize.x * t, baseSize.y);
            float leftOffset = (baseSize.x - baseSize.x * t) * 0.5f;
            fillTransform.localPosition = new Vector3(-leftOffset, fillTransform.localPosition.y, fillTransform.localPosition.z);
        }
        else
        {
            fillTransform.localScale = new Vector3(baseScale.x * t, baseScale.y, baseScale.z);
            if (fillSR != null) // corrección de anclaje si pivot centrado
            {
                float leftOffset = (baseScale.x - baseScale.x * t) * 0.5f * fillSR.sprite.bounds.size.x;
                fillTransform.localPosition = new Vector3(-leftOffset, fillTransform.localPosition.y, fillTransform.localPosition.z);
            }
        }

        showTimer = showSecondsOnChange;
        if (hideWhenFull)
            gameObject.SetActive(t < 1f || showSecondsOnChange > 0f);
        else
            gameObject.SetActive(true);
    }
}
