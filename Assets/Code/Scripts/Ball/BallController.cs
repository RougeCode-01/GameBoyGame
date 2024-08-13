using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour, IBall
{
    [SerializeField] private float ballSpeed;
    [SerializeField] private float ballMaxSpeed;
    [SerializeField] private float respawnDelay = 2.0f;
    private float currentSpeed;
    private Rigidbody2D _rb;
    private Vector2 _startingPosition;

    private void OnEnable()
    {
        AudioManager.instance.PlaySFX("ShootingBall");
        _rb = GetComponent<Rigidbody2D>();
        _startingPosition = Vector2.zero;
        transform.position = _startingPosition;
        Move();
    }

    private void FixedUpdate()
    {
        ClampSpeed();
    }

    public void Move()
    {
        float randomAngle = Random.Range(-45f, 45f);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        _rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);
        currentSpeed = ballSpeed;
    }

    public void ClampSpeed()
    {
        if (_rb.velocity.magnitude > ballMaxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * ballMaxSpeed;
        }
    }

    public void Respawn()
    {
        StartCoroutine(DeactivateAndRespawnBall());
    }

    private IEnumerator DeactivateAndRespawnBall()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        transform.position = _startingPosition;
        _rb.velocity = Vector2.zero;
        currentSpeed = ballSpeed;
        gameObject.SetActive(true);
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall") || other.CompareTag("TopWall"))
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.instance.PlaySFX("BallBounces");
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("TopWall"))
        {
            AddRandomAngle();
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            AddRandomAngle();
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

    public void AddRandomAngle() // Change to public
    {
        float randomAngle = Random.Range(-10f, 10f);
        _rb.velocity = Quaternion.Euler(0, 0, randomAngle) * _rb.velocity;
    }
}