using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Brick : MonoBehaviour
{
    // Number of hits required to break the brick
    public int hitPoints = 1;

    private int currentHits = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            currentHits++;
            if (currentHits >= hitPoints)
            {
                Destroy(gameObject);
            }
        }
    }
}

