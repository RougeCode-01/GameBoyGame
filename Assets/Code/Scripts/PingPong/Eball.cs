using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eball : MonoBehaviour
{

    public float speed = 5.0f;
    private Rigidbody2D rb;
    public Transform playerPaddle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
 //       playerPaddle = GameObject.FindGameObjectWithTag("PlayerPaddle").transform;
        SpawnBall();
    }

    void SpawnBall()
    {
        // Spawn the ball on top of the player paddle
        transform.position = new Vector2(playerPaddle.position.x, playerPaddle.position.y + 0.1f);
        LaunchBall();
    }

    void LaunchBall()
    {
        AudioManager.instance.PlaySFX("ShootingBall");
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        float x = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            // Adjust the ball's velocity based on the Enemy's velocity
            Vector2 direction = rb.velocity;
            direction.x = direction.x + (collision.collider.attachedRigidbody.velocity.x / 2);

            // Ensure the speed remains constant
            rb.velocity = direction.normalized * speed;

            // If the collision is with the player paddle specifically, apply additional logic if needed
            if (collision.collider.CompareTag("Player"))
            {
                // Example: add a slight random angle to the bounce to make it less predictable
                float randomAngle = Random.Range(-0.1f, 0.1f);
                rb.velocity = Quaternion.Euler(0, 0, randomAngle) * rb.velocity;
            }
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // Bounce off the walls without changing the speed
            Vector2 direction = rb.velocity;
            AudioManager.instance.PlaySFX("BallBounces");
            if (collision.contacts[0].normal.y != 0) // Horizontal walls
            {
                direction.y = -direction.y;
            }
            else if (collision.contacts[0].normal.x != 0) // Vertical walls
            {
                direction.x = -direction.x;
            }
            rb.velocity = direction.normalized * speed;
        }
    }
}

