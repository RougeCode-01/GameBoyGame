using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform ball;

    void Update()
    {
        // Calculate target position on the same y-axis as the AI paddle
        float targetX = ball.position.x;

        // Move AI towards the target x-position
        Vector2 newPosition = new Vector2(targetX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }
}