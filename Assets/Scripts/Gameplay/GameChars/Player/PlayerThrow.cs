using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : GameCharThrow
{
    private void OnThrow(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Debug.Log("Throw button pressed.");
        }
        else
        {
            Debug.Log("Throw button released.");
        }
    }
}
