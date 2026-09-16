using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : GameCharMove
{
    private void OnMove(InputValue inputValue)
    {
        MoveDirection = inputValue.Get<Vector2>();
        Debug.Log($"Move: <{MoveDirection.x},{MoveDirection.y}>");
    }

    private void OnDodge(InputValue inputValue)
    {
        Debug.Log("Dodge button pressed.");
    }
}
