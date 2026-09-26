using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private bool isAlive;
    [SerializeField] private float velocityDecayRate = 1.5f; // Units / s^2
    [SerializeField] private int maxBouncesTilDeath = 3;
    [SerializeField] private float minAliveSpeed = 4.0f;
    [SerializeField] private Sprite ballAliveSprite, ballDeadSprite;
    [SerializeField] private float hitDamage = 10;

    private int bounces;
    private float speed;
    private Vector2 direction;
    private GameObject thrownBy;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        bounces = 0;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Decay velocity
        speed = Mathf.Clamp(
            speed - velocityDecayRate * Time.deltaTime, 
            0, 
            speed);
        rb.linearVelocity = rb.linearVelocity.normalized * speed;

        // Set velocity
        rb.linearVelocity = direction * speed;

        // Kill balls
        if (bounces >= maxBouncesTilDeath
            || rb.linearVelocity.magnitude < minAliveSpeed)
        {
            isAlive = false;
        }

        // Set sprite
        spriteRenderer.sprite = isAlive ? ballAliveSprite : ballDeadSprite;
    }

    // Use Initialize() rather than Start() for setup
    // Since parameters will need to be passed
    public void Initialize(bool isAlive, Vector2 dir, float speed, GameObject thrownBy)
    {
        this.isAlive = isAlive;
        this.direction = dir;
        this.speed = speed;
        this.thrownBy = thrownBy;

        // RigidBody2D will be grabbed in Start()
        // Since Initialize() is not called by dead balls
        // As such, we will cache direction & speed
        // And calculate velocity during Update()
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Ball-GameChar collisions
        if (collider.gameObject.layer == LayerMask.NameToLayer("GameChars"))
        {
            // Deal damage and recoil
            if (isAlive)
            {
                // Prevent colliding with the thrower
                if (bounces == 0 && thrownBy == collider.gameObject)
                {
                    return;
                }

                if (collider.gameObject.TryGetComponent<GameCharHealth>(out GameCharHealth gameCharHealth))
                {
                    gameCharHealth.Health = Mathf.Clamp(gameCharHealth.Health - hitDamage, 0, gameCharHealth.Health);
                }

                // Change ball direction
                // Flip vertically if ball is above/below collider
                if (transform.position.y > collider.gameObject.transform.position.y + collider.gameObject.transform.localScale.x / 2
                    || transform.position.y < collider.gameObject.transform.position.y - collider.gameObject.transform.localScale.x / 2)
                {
                    Debug.Log("Hit from above/below.");
                    direction = new Vector2(direction.x, -direction.y);
                }
                else
                {
                    Debug.Log("Hit from side.");
                    direction = new Vector2(-direction.x, direction.y);
                }

                isAlive = false;
            }

            // Pickup ball
            else
            {
                if (collider.gameObject.TryGetComponent<GameCharThrow>(out GameCharThrow gameCharThrow)
                    && !gameCharThrow.IsHoldingBall) // GameChar can only hold one ball at a time
                {
                    gameCharThrow.IsHoldingBall = true;
                    Destroy(gameObject);
                }
            }
        }

        // Ball-BallWall collisions
        else if (collider.gameObject.layer == LayerMask.NameToLayer("BallWalls"))
        {
            bounces++;
            if (collider.gameObject.name == "NorthWall" || collider.gameObject.name == "SouthWall")
            {
                direction = new Vector2(direction.x, -direction.y);
            }
            else // East/WestWall
            {
                direction = new Vector2(-direction.x, direction.y);
            }
        }
    }
}
