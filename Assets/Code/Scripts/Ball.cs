using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballSpeed; // Speed of the ball
    [SerializeField] private float ballMaxSpeed; // Maximum speed of the ball
    
    private Rigidbody2D _rb; // Reference to the Rigidbody2D component
    private Vector2 _sartingPosition; // Starting position of the ball
    
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sartingPosition = transform.position;
        BallMovement();
    }

    private void FixedUpdate()
    {
        //clamp the speed of the ball
        if (_rb.velocity.magnitude > ballMaxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * ballMaxSpeed;
        }
    }

    public void BallMovement()
    {
        //Starting the ball movement
        float y = UnityEngine.Random.value < 0.5f ? -1f : 1f;
        //Random side to side movement
        float x = UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1f, -0.5f)
            : UnityEngine.Random.Range(0.5f, 1f);
        // Apply the initial force and set the current speed
        Vector2 direction = new Vector2(x, y).normalized;
        _rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other)// Check if the ball collides with the bottom wall and the top wall
    {
        // Check if the ball collides with the bottom wall and the top wall
        if (other.CompareTag("BottomWall") || other.CompareTag("TopWall"))
        {
           DeactivateBall();
        }
    }
    
    private void DeactivateBall()// Deactivate the ball and reset its position
    {
        gameObject.SetActive(false);
        transform.position = _sartingPosition;
        _rb.velocity = Vector2.zero;
    }
}