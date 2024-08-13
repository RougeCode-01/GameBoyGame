using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Brick> bricks;

    void Start()
    {
        // Find all brick objects in the scene and add them to the bricks list
        bricks = FindObjectsOfType<Brick>().ToList();
    }

    void Update()
    {
        // Remove destroyed bricks from the list
        bricks.RemoveAll(brick => brick == null);

        // Load the boss scene if all bricks are destroyed
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