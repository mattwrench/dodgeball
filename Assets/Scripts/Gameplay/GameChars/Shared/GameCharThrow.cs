using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCharThrow : MonoBehaviour
{
    public bool IsHoldingBall;

    protected Vector2 AimDirection;
    protected bool IsThrowingBall;

    [SerializeField] private float maxThrowTimer = 2.0f;
    [SerializeField] private float minThrowSpeed = 8f;
    [SerializeField] private float maxThrowSpeed = 16f;
    [SerializeField] private GameObject ballAlivePrefab;

    private float throwTimer;

    public float ThrowCharge
    {
        get
        {
            return throwTimer / maxThrowTimer;
        }
    }

    private void Start()
    {
        IsHoldingBall = false;
        throwTimer = 0;
        AimDirection = Vector2.zero;
    }

    protected virtual void Update()
    {
        // Update timer
        if (IsThrowingBall)
        {
            throwTimer = Mathf.Clamp(throwTimer + Time.deltaTime, 0, maxThrowTimer);
        }

        // Throw ball
        // Guarantee AimDirection is non-zero to avoid spawning a stationary ball
        if (!IsThrowingBall && throwTimer > 0 && AimDirection.sqrMagnitude > 0)
        {
            float throwSpeed = (maxThrowSpeed - minThrowSpeed) * ThrowCharge + minThrowSpeed;
            SpawnBall(AimDirection, throwSpeed);
            throwTimer = 0;
            IsHoldingBall = false;
        }
    }

    private void SpawnBall(Vector2 dir, float speed)
    {
        GameObject newBall = Instantiate(ballAlivePrefab, transform.position, Quaternion.identity);
        if (newBall.TryGetComponent<BallController>(out BallController ballController))
        {
            ballController.Initialize(true, dir * speed);
        }
    }
}
