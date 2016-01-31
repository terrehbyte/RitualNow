using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Image[] lives;
    public Packer player;

    public Color heartActive;
    public Color heartInactive;

    public GameObject GameOverPanel;

    public Text ParcelCount;
    public Text TimeDisplay;

    void Update()
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

        if (player.LivesCount <= 0 && !GameOverPanel.activeInHierarchy)
        {
            GameOverPanel.SetActive(true);

            float timeElapsed = Statistics.instance.TimeElapsed;
                
            ParcelCount.text = Statistics.instance.ParcelPlacementCount.ToString("D3") + "/" + Statistics.instance.ParcelSpawnCount.ToString("D3");
            TimeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", timeElapsed / 3600, (timeElapsed / 60) % 60, timeElapsed % 60);
        }
    }

    // utility functiosn
    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }


}
