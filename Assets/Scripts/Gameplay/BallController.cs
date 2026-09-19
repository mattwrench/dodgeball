using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private bool isAlive;

    private void Start()
    {
        
    }

    private void Update()
    {
        
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
            // TODO
        }
    }
}
