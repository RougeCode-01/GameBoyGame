using System.Collections;
using System.Collections.Generic;
using GBTemplate;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    private GBConsoleController _gb;//Getting the instance of the console controller, so we can access its functions
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _gb = GBConsoleController.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        //Move left and right
        if (_gb.Input.Left)
        {
            MovePlayer(Vector2.left);
        }
        else if(_gb.Input.Right)
        {
            MovePlayer(Vector2.right);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        transform.Translate(direction * (movementSpeed * Time.deltaTime));
    }

    private void HandleAttack()
    {
        //Button A to shoot
        if (_gb.Input.ButtonAJustPressed)
        {
            //fire a bullet
        }    
    }
}
