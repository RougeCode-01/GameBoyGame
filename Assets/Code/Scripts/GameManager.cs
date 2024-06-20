using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bricks;

    void Start()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick1").ToList();
        bricks.AddRange(GameObject.FindGameObjectsWithTag("Brick2"));
        bricks.AddRange(GameObject.FindGameObjectsWithTag("Brick3"));
    }

    void Update()
    {
        bricks.RemoveAll(brick => brick == null);

        if (bricks.Count == 0)
        {
            LoadBossScene();
        }
    }

    void LoadBossScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}