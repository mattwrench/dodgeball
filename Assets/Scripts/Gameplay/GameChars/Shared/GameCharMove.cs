using UnityEngine;

public class GameCharMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;

    protected Vector2 MoveDirection;

    private Rigidbody2D rb;

    private void Start()
    {
        MoveDirection = Vector2.zero;
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = MoveDirection * moveSpeed;
    }
}
