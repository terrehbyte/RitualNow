using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Image[] lives;
    public Packer player;

    public Color heartActive;
    public Color heartInactive;

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
    }
}
