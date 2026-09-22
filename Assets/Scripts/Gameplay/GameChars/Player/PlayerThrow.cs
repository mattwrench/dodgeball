using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : GameCharThrow
{
    private void OnThrow(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Debug.Log("Throw button pressed.");
            if (IsHoldingBall)
            {
                IsThrowingBall = true;
            }
        }
        else
        {
            Debug.Log("Throw button released.");
            IsThrowingBall = false;
        }
    }
}
