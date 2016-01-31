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

        if (player.LivesCount <= 0)
        {
            GameOverPanel.SetActive(true);
        }
    }
}
