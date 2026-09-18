using System.Collections;
using UnityEngine;

public class GameCharAnimate : MonoBehaviour
{
    public enum Direction
    {
        Left, Right, Up, Down
    }

    private Direction direction;

    private Animator animator;
    private Rigidbody2D rb;
    private GameCharState gameCharState;
    private GameCharThrow gameCharThrow;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameCharState = GetComponent<GameCharState>();
        gameCharThrow = GetComponent<GameCharThrow>();

        // Set starting direction based on team
        // Since gameChar is not moving on the first frame,
        // SetAnimation() needs to be explicitly called
        direction = gameCharState.Side == GameCharState.Team.Left ? Direction.Right : Direction.Left;
        SetAnimation();
    }

    private void Update()
    {
        // Do not change animation if gameChar is not moving
        if (rb.linearVelocity.sqrMagnitude < Mathf.Epsilon)
        {
            animator.speed = 0;
            return;
        }

        // Determine direction
        // Horizontal movement takes precedence over vertical
        if (Mathf.Abs(rb.linearVelocity.x) >= Mathf.Abs(rb.linearVelocity.y))
        {
            direction = rb.linearVelocity.x > 0 ? Direction.Right : Direction.Left;

        }
        else
        {
            direction = rb.linearVelocity.y > 0 ? Direction.Up : Direction.Down;
        }

        animator.speed = 1;
        SetAnimation();
    }

    private void SetAnimation()
    {
        switch (direction)
        {
            case Direction.Right:
                animator.Play(gameCharThrow.IsHoldingBall ? "MoveRightBall" : "MoveRight");
                break;
            case Direction.Left:
                animator.Play(gameCharThrow.IsHoldingBall ? "MoveLeftBall" : "MoveLeft");
                break;
            case Direction.Up:
                animator.Play(gameCharThrow.IsHoldingBall ? "MoveUpBall" : "MoveUp");
                break;
            case Direction.Down:
                animator.Play(gameCharThrow.IsHoldingBall ? "MoveDownBall" : "MoveDown");
                break;
        }
    }
}
