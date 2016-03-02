using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

using Zenject;

public class HUD : MonoBehaviour
{
    [Inject]
    Statistics _stats;

    [Inject]
    Packer player;

    [Inject]
    GameMode _game;

    public Image[] lives;

    public Color heartActive;
    public Color heartInactive;

    public GameObject GameOverPanel;

    public Text ParcelCount;
    public Text TimeDisplay;
    public Text ScoreDisplay;

    void FixedUpdate()
    {
        // Lives
        for (int i = 0; i < lives.Length; ++i)
        {
            if (i < player.LivesCount)
            {
                lives[i].color = heartActive; 
            }
            else
            {
                lives[i].color = heartInactive;
            }
        }

        // Score
        ScoreDisplay.text = _stats.Score.ToString("D8");

        if (player.LivesCount <= 0 && !GameOverPanel.activeInHierarchy)
        {
            GameOverPanel.SetActive(true);

            float timeElapsed = _stats.TimeElapsed;
                
            ParcelCount.text = _stats.ParcelPlacementCount.ToString("D3") + "/" + _stats.ParcelSpawnCount.ToString("D3");
            TimeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", timeElapsed / 3600, (timeElapsed / 60) % 60, timeElapsed % 60);
        }
    }

    public void StartGame()
    {
        _game.StartGame();
    }

    // utility functions

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
