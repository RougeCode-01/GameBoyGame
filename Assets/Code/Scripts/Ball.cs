using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballSpeed; // Speed of the ball
    [SerializeField] private float ballMaxSpeed; // Maximum speed of the ball
    [SerializeField] private float respawnDelay = 2.0f; // Delay before the ball respawns
    private float currentSpeed; // Current speed of the ball

    private Rigidbody2D _rb; // Reference to the Rigidbody2D component
    private Vector2 _startingPosition; // Starting position of the ball

    private void OnEnable()
    {
        AudioManager.instance.PlaySFX("ShootingBall");
        _rb = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
        BallMovement();
    }

    private void FixedUpdate()
    {
        ClampBallSpeed();
    }

    private void ClampBallSpeed()
    {
        if (_rb.velocity.magnitude > ballMaxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * ballMaxSpeed;
        }
    }

    public void BallMovement()
    {
        // Get a random direction for the ball
        float randomAngle = UnityEngine.Random.Range(-45f, 45f);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;

        // Apply the initial force and set the current speed
        _rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);

        // Set the current speed to the base speed
        currentSpeed = ballSpeed;
    }

    private void RandomAngle()
    {
        // Add a slight random angle to the direction
        float randomAngle = UnityEngine.Random.Range(-10f, 10f);
        _rb.velocity = Quaternion.Euler(0, 0, randomAngle) * _rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other) // Check if the ball collides with the bottom wall and the top wall
    {
        // Check if the ball collides with the bottom wall and the top wall
        if (other.CompareTag("BottomWall") || other.CompareTag("TopWall"))
        {
            StartCoroutine(DeactivateAndRespawnBall());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) // Check if the ball collides with the player paddle
    {
        AudioManager.instance.PlaySFX("BallBounces");
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("TopWall"))
        {
            RandomAngle();
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            RandomAngle();
        }
    }

    private IEnumerator DeactivateAndRespawnBall() // Deactivate the ball and reset its position
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        transform.position = _startingPosition;
        _rb.velocity = Vector2.zero;
        currentSpeed = ballSpeed; // Reset the speed
        gameObject.SetActive(true);
        BallMovement();
    }
}