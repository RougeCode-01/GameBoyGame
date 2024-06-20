using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;

public class MainMenu : MonoBehaviour
{
    private GBConsoleController _gb;

    private void Start()
    {
        _gb = GBConsoleController.GetInstance();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(_gb.Input.ButtonStart)
        {
            StartGame();
        }
        if(_gb.Input.ButtonSelect)
        {
            Quit();
        }
    }
}
