using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCharThrow : MonoBehaviour
{
    public bool IsHoldingBall;
    public bool IsThrowingBall;

    protected Vector2 AimDirection;

    [SerializeField] private float maxThrowTimer = 2.0f;
    [SerializeField] private float minThrowSpeed = 8f;
    [SerializeField] private float maxThrowSpeed = 24f;
    [SerializeField] private GameObject ballAlivePrefab;

    private float ThrowTimer;

    public float ThrowCharge
    {
        get
        {
            return ThrowTimer / maxThrowTimer;
        }
    }

    private void Start()
    {
        IsHoldingBall = false;
        ThrowTimer = 0;
        AimDirection = Vector2.zero;
    }

    protected virtual void Update()
    {
        // Update timer
        if (IsThrowingBall)
        {
            ThrowTimer = Mathf.Clamp(ThrowTimer + Time.deltaTime, 0, maxThrowTimer);
        }

        // Throw ball
        // Guarantee AimDirection is non-zero to avoid spawning a stationary ball
        if (!IsThrowingBall && ThrowTimer > 0 && AimDirection.sqrMagnitude > 0)
        {
            float throwSpeed = (maxThrowSpeed - minThrowSpeed) * ThrowCharge + minThrowSpeed;
            SpawnBall(AimDirection, throwSpeed);
            ThrowTimer = 0;
            IsHoldingBall = false;
        }
    }

    private void SpawnBall(Vector2 dir, float speed)
    {
        GameObject newBall = Instantiate(ballAlivePrefab, transform.position, Quaternion.identity);
        if (newBall.TryGetComponent<BallController>(out BallController ballController))
        {
            ballController.Initialize(true, dir, speed);
        }
    }
}
