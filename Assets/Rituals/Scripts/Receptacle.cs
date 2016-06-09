using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

using Zenject;

[System.Serializable]
public class ItemSetDisplay
{
    public Sprite AcceptedIcon;
    public string AcceptedTag;
}

public class Receptacle : MonoBehaviour
{
    [Inject]
    Statistics _stats;

    [Inject]
    Packer     _player;

    public ItemSetDisplay[] AcceptedItems;

    private string CurrentAcceptedType;
    [SerializeField]
    private SpriteRenderer desiredItemImage;

    private int NumberAccepted;
    public int NumberNeeded = 1;

    public UnityEvent OnReceptacleFull = new UnityEvent();

    public void Start()
    {
        Scramble();
    }

    public void Scramble()
    {
        NumberAccepted = 0;

        ItemSetDisplay newAcceptedItem = AcceptedItems[Random.Range(0, AcceptedItems.Length - 1)];

        desiredItemImage.sprite = newAcceptedItem.AcceptedIcon;
        CurrentAcceptedType = newAcceptedItem.AcceptedTag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool validDrop = false;

        var itemID = other.GetComponent<ItemIdentifier>();

        if (itemID == null)
        {
            return;
        }

        if (itemID.item.tag == CurrentAcceptedType)
        {
            _stats.ParcelPlacementCount += 1;

            if (_stats.ParcelPlacementCount % 10 == 0)
            {
                _player.TakeDamage(-1);
            }

            validDrop = true;
        }

        if (!validDrop)
        {
            _player.TakeDamage(1);
        }
        else
        {
            _stats.Score += 10; // TODO: different values for diff packs
            NumberAccepted += 1;

            if (NumberAccepted >= NumberNeeded)
            {
                OnReceptacleFull.Invoke();
            }
        }

        Destroy(other.gameObject);
    }
}