using UnityEngine;

public class Ball : MonoBehaviour
{
    private IBall _ballController;

    private void Awake()
    {
        _ballController = GetComponent<IBall>();
    }

    private void OnEnable()
    {
        _ballController.Move();
    }

    private void FixedUpdate()
    {
        _ballController.ClampSpeed();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall") || other.CompareTag("TopWall"))
        {
            _ballController.Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.instance.PlaySFX("BallBounces");
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("TopWall"))
        {
            _ballController.AddRandomAngle();
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            _ballController.AddRandomAngle();
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            IBrick brick = other.gameObject.GetComponent<IBrick>();
            if (brick != null)
            {
                brick.TakeDamage(1);
            }
        }
    }
}