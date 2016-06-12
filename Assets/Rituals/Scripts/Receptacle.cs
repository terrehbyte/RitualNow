using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

using Zenject;

[System.Serializable]
public class ItemEvent : UnityEvent<Item, int> { }

public class Receptacle : MonoBehaviour
{
    [Inject]
    Statistics _stats;

    [Inject]
    Packer     _player;

    [Inject]
    ItemDatabase _items;

    // TODO: change this to use ItemHandle objects, which are fancy resolvers for item IDs
    // TODO: this means implement ItemHandle so you can have ONE location for items...
    // TODO: OR, expose the ItemStore to Zenject and have that injected instead...
    [SerializeField]
    private int[] AcceptedItems;
    private Item CurrentAcceptedItem;

    // help this naming is bonkers
    private int NumberAccepted;
    public int NumberDesired = 2;

    public int NumberNeeded
    {
        get
        {
            return NumberDesired - NumberAccepted;
        }
    }

    // Called when the receptacle's conditions have changed.
    // - arg0 :: Item needed
    // - arg1 :: Qty needed
    public ItemEvent OnReceptacleChange = new ItemEvent();
    public UnityEvent OnReceptacleFull = new UnityEvent();

    public void Start()
    {
        Scramble();
    }

    public void Scramble()
    {
        NumberAccepted = 0;
        CurrentAcceptedItem = _items.data[AcceptedItems[Random.Range(0, AcceptedItems.Length - 1)]];
        OnReceptacleChange.Invoke(CurrentAcceptedItem, NumberNeeded);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool validDrop = false;

        var itemID = other.GetComponent<ItemIdentifier>();

        if (itemID == null)
        {
            return;
        }

        if (itemID.item.tag == CurrentAcceptedItem.tag)
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

            OnReceptacleChange.Invoke(CurrentAcceptedItem, NumberNeeded);

            if (NumberAccepted >= NumberDesired)
            {
                OnReceptacleFull.Invoke();
            }
        }

        Destroy(other.gameObject);
    }
}