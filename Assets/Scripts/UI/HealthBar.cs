using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    private readonly Color HighHealthColor = Color.green;
    private readonly Color MediumHealthColor = Color.yellow;
    private readonly Color LowHealthColor = Color.red;
    private const float MediumHealthThreshold = .7f;
    private const float LowHealthThreshold = .4f;

    private Slider slider;
    private GameCharHealth gameCharHealth;
    
    private void Start()
    {
        slider = GetComponent<Slider>();
        gameCharHealth = GetComponentInParent<GameCharHealth>();    
    }

    private void Update()
    {
        float healthRatio = gameCharHealth.Health / gameCharHealth.MaxHealth;

        // Set slider length and color based on health
        slider.value = healthRatio;
        fill.color = healthRatio switch
        {
            < LowHealthThreshold => LowHealthColor,
            < MediumHealthThreshold => MediumHealthColor,
            _ => HighHealthColor
        };
    }
}
