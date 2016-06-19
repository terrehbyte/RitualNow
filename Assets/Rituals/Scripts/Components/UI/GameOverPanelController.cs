using UnityEngine;
using UnityEngine.UI;

using Zenject;
namespace RitualWarehouse
{
    public class GameOverPanelController : MonoBehaviour
    {
        [Inject]
        Statistics _stats;

        [Inject]
        Packer player;

        [Inject]
        GameMode _game;

        public Text ParcelCount;
        public Text TimeDisplay;

        void OnEnable()
        {
            float timeElapsed = _stats.TimeElapsed;

            ParcelCount.text = _stats.ParcelPlacementCount.ToString("D3") + "/" + _stats.ParcelSpawnCount.ToString("D3");
            TimeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", timeElapsed / 3600, (timeElapsed / 60) % 60, timeElapsed % 60);
        }
    }
}