using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    private int bricksHealth = 2; // Health of the brick
    private int points = 100;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (bricksHealth == 1)
        {
            _spriteRenderer.color = Color.red;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            AudioManager.instance.PlaySFX("BrickBreaks");
            bricksHealth--;
            if (bricksHealth == 0)
            {
                OnDestroy();
            }
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
