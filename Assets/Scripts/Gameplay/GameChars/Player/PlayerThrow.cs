using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : GameCharThrow
{
    private Vector2 mouseScreenPos;

    protected override void Update()
    {
        // Update AimDirection
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        AimDirection = (mouseWorldPos - (Vector2)transform.position).normalized;
        //Debug.Log($"Aim: <{AimDirection.x},{AimDirection.y}>");

        base.Update();
    }


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

    private void OnAim(InputValue inputValue)
    {
        // Only save the mouse screen position
        // Must recalculate AimDirection every frame
        // Because player may have moved but mouse may be stationary
        mouseScreenPos = inputValue.Get<Vector2>();
        //Debug.Log($"Mouse screen position: <{mouseScreenPos.x},{mouseScreenPos.y}>");
    }
}
