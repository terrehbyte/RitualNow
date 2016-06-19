using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

using Zenject;

namespace RitualWarehouse
{
    public class HUD : MonoBehaviour
    {
        [Inject]
        Statistics _stats;

        [Inject]
        Packer player;

        [Inject]
        GameMode _game;

        public GameObject GameOverPanel;

        public Text ScoreDisplay;
        public Text LivesDisplay;

        void FixedUpdate()
        {
            LivesDisplay.text = player.LivesCount.ToString("D2");
            ScoreDisplay.text = _stats.Score.ToString("D8");

            // Display game over panel w/ statistics
            if (player.LivesCount <= 0 && !GameOverPanel.activeInHierarchy)
            {
                GameOverPanel.SetActive(true);
            }
        }

        // utility functions

        public void StartGame()
        {
            _game.StartGame();
        }

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
}