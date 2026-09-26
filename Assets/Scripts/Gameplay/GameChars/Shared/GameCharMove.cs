using UnityEngine;

public class GameCharMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;

    protected Vector2 MoveDirection;

    private Rigidbody2D rb;
    private GameCharThrow gameCharThrow;

    private void Start()
    {
        MoveDirection = Vector2.zero;
        
        rb = GetComponent<Rigidbody2D>();
        gameCharThrow = GetComponent<GameCharThrow>();
    }

    private void Update()
    {
        // Stop movement when throwing ball
        if (gameCharThrow.IsThrowingBall)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = MoveDirection * moveSpeed;
    }
}
