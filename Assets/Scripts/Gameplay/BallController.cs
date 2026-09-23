using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private bool isAlive;

    private Rigidbody2D rb;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // Use Initialize() rather than Start() for setup
    // Since parameters will need to be passed
    public void Initialize(bool isAlive, Vector2 velocity)
    {
        this.isAlive = isAlive;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ball-GameChar collisions
        if (collision.gameObject.layer == LayerMask.NameToLayer("GameChars"))
        {
            // Deal damage and recoil
            if (isAlive)
            {
                // TODO
            }

            // Pickup ball
            else
            {
                if (collision.gameObject.TryGetComponent<GameCharThrow>(out GameCharThrow gameCharThrow)
                    && !gameCharThrow.IsHoldingBall) // GameChar can only hold one ball at a time
                {
                    gameCharThrow.IsHoldingBall = true;
                    Destroy(gameObject);
                }
            }
        }

        // Ball-BallWall collisions
        else if (collision.gameObject.layer == LayerMask.NameToLayer("BallWalls"))
        {
            if (collision.gameObject.name == "NorthWall" || collision.gameObject.name == "SouthWall")
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
            }
            else // East/WestWall
            {
                rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
            }
        }
    }
}
