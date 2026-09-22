using System;
using UnityEngine;

public class GameCharThrow : MonoBehaviour
{
    public bool IsHoldingBall;
    public bool IsThrowingBall;
    public float ThrowTimer;
    public float MaxThrowTime = 2.0f;

    private void Start()
    {
        IsHoldingBall = false;
        ThrowTimer = 0;
    }

    private void Update()
    {
        // Update timer
        if (IsThrowingBall)
        {
            ThrowTimer = Mathf.Clamp(ThrowTimer + Time.deltaTime, 0, MaxThrowTime);
        }

        // Throw ball
        if (!IsThrowingBall && ThrowTimer > 0)
        {
            ThrowTimer = 0;
            IsHoldingBall = false;
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        // TODO
    }
}
