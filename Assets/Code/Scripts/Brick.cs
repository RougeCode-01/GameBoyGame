using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}