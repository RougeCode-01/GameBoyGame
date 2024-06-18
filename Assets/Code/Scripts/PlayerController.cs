using System.Collections;
using System.Collections.Generic;
using GBTemplate;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed; // Movement speed of the player
    [SerializeField] private GameObject ball;// Reference to the Ball script
    
    private Ball _ball; // Reference to the Ball script

    public int maxHealth = 3; // Maximum player health

    private GBConsoleController _gb; // Reference to the GBConsoleController
    
    private Camera _mainCamera; // Reference to the main camera

    private float _xMin, _xMax, _yMin, _yMax; // Bounds for player movement

    void Start()
    {
        _mainCamera = Camera.main;// Get the main camera
        _gb = GBConsoleController.GetInstance();// Get the GBConsoleController instance

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
        HandleMovement(); // Handle player movement
        ReactivateBall(); // Reactivate the ball
    }

    private void HandleMovement()
    {
        // Move left and right based on input
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

        // Apply speed and clamp position within bounds
        float xValidPosition = Mathf.Clamp(transform.position.x + movement.x, _xMin, _xMax);
        float yValidPosition = Mathf.Clamp(transform.position.y + movement.y, _yMin, _yMax);
        transform.position = new Vector3(xValidPosition, yValidPosition, 0f);
    }

    private void ReactivateBall()
    {
        if (_gb.Input.ButtonAJustPressed)
        {
            ActivateBall();
        }
    }
    
    public void ActivateBall()// Activate the ball and apply a random force
    {
        ball.SetActive(true);
    }
}
