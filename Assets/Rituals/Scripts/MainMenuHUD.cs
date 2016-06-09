﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuHUD : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
