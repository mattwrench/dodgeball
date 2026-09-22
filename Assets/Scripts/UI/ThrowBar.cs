using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    // Transition from gray to white
    private readonly float MaxThrowColor = 1f;
    private readonly float MinThrowColor = 0.5f;

    private Slider slider;
    private CanvasGroup canvasGroup;
    private GameCharThrow gameCharThrow;
    private GameCharState gameCharState;

    private void Start()
    {
        slider = GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameCharThrow = GetComponentInParent<GameCharThrow>();
        gameCharState = GetComponentInParent<GameCharState>();

        // Flip throw bar horizontally if on right team
        if (gameCharState.Side == GameCharState.Team.Right)
        {
            transform.localPosition = new Vector3(
                -transform.localPosition.x, 
                transform.localPosition.y, 
                transform.localPosition.z);
        }
    }

    private void Update()
    {
        if (gameCharThrow.IsThrowingBall)
        {
            canvasGroup.alpha = 1;

            float throwRatio = gameCharThrow.ThrowTimer / gameCharThrow.MaxThrowTime;

            // Set slider length and color based on health
            slider.value = throwRatio;
            float fillColor = (MaxThrowColor - MinThrowColor) * throwRatio + MinThrowColor;
            fill.color = new Color(fillColor, fillColor, fillColor);
        }

        // Hide throw bar if not throwing
        else
        {
            canvasGroup.alpha = 0;
        }
    }
}
