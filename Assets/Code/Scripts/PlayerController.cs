using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    
    [SerializeField] private GameObject bigbulletPrefab;
    [SerializeField] private float bigbulletSpeed;
    [SerializeField] private float bigbulletLifeTime;
    private float _bigBulletFireRate;
    
    private GBConsoleController _gb;
    private Camera _mainCamera;

    private bool _isFiring;

    private float _xMin, _xMax, _yMin, _yMax; // Bounds for player movement

    void Start()
    {
        _mainCamera = Camera.main;
        _gb = GBConsoleController.GetInstance();

        // Calculate screen bounds based on camera size
        var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        var camHeight = _mainCamera.orthographicSize;
        var camWidth = camHeight * _mainCamera.aspect;

        // Set bounds for player movement
        _yMin = -camHeight + spriteSize;
        _yMax = camHeight - spriteSize;
        _xMin = -camWidth + spriteSize;
        _xMax = camWidth - spriteSize;
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        // Move left and right
        if (_gb.Input.Left)
        {
            MovePlayer(Vector2.left);
        }
        else if (_gb.Input.Right)
        {
            MovePlayer(Vector2.right);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        // Calculate movement
        Vector2 movement = direction * (movementSpeed * Time.deltaTime);

        // Apply speed and clamp position
        float xValidPosition = Mathf.Clamp(transform.position.x + movement.x, _xMin, _xMax);
        float yValidPosition = Mathf.Clamp(transform.position.y + movement.y, _yMin, _yMax);
        transform.position = new Vector3(xValidPosition, yValidPosition, 0f);
    }

    private void HandleAttack()
    {
        // Button A to shoot
        if (_gb.Input.ButtonAJustPressed)
        {
            SpawnBullet();
        }
        else if (_gb.Input.ButtonB && _bigBulletFireRate <= 0f)
        {
            SpawnBigBullet();
            _bigBulletFireRate = 5f;
        }

        if (_bigBulletFireRate > 0f)
        {
            _bigBulletFireRate -= Time.deltaTime;
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = bulletSpeed * transform.up;
            
        Destroy(bullet, bulletLifeTime);
    }
    
    private void SpawnBigBullet()
    {
        GameObject bigBullet = Instantiate(bigbulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = bigBullet.GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = bigbulletSpeed * transform.up;
            
        Destroy(bigBullet, bigbulletLifeTime);
    }
}
