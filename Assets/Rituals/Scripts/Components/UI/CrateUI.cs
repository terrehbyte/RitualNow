using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RitualWarehouse
{
    public class CrateUI : MonoBehaviour
    {
        [SerializeField]
        private Receptacle TargetReceptacle;

        [SerializeField]
        private Image ItemDisplay;
        [SerializeField]
        private Text ItemQtyDisplay;

        // TODO: Expose things on the receptacle as events
        void Start()
        {
            TargetReceptacle.OnReceptacleChange.AddListener(OnReceptacleChanged);
        }

        void OnReceptacleChanged(Item targetItemNeeded, int targetQtyNeeded)
        {
            ItemDisplay.sprite = targetItemNeeded.image;
            ItemQtyDisplay.text = "x " + targetQtyNeeded.ToString("D2");
        }
    }
}