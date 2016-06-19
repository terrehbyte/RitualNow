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

    // Total number of items recieved that were valid for this receptacle.
    private int NumberAccepted;

    // Total number of items needed to fill the receptacle.
    public int NumberDesired = 2;

    // Remaining number of items needed to fill the receptacle.
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

    // Called when the receptacle's conditions have been fulfilled.
    public UnityEvent OnReceptacleFull = new UnityEvent();

    // Changes the requirements for this receptacle and resets any running totals.
    public void Scramble()
    {
        NumberAccepted = 0;
        CurrentAcceptedItem = _items.data[AcceptedItems[Random.Range(0, AcceptedItems.Length - 1)]];
        OnReceptacleChange.Invoke(CurrentAcceptedItem, NumberNeeded);
    }

    // Returns true if the evaluated item is accepted by this receptacle, otherwise returns false.
    public bool ValidateItem(Item evaluatedItem)
    {
        return CurrentAcceptedItem.tag == evaluatedItem.tag;
    }

    private void OnItemAccepted(Item itemStillNeeded, int qtyStillNeeded)
    {
        if (NumberAccepted >= NumberDesired)
        {
            OnReceptacleFull.Invoke();
        }
    }

    private void Awake()
    {
        OnReceptacleChange.AddListener(OnItemAccepted);
    }
    private void Start()
    {
        Scramble();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var itemID = other.GetComponent<ItemIdentifier>();
        if (itemID == null) { return; }

        if (ValidateItem(itemID.item))
        {
            _stats.ParcelPlacementCount += 1;

            if (_stats.ParcelPlacementCount % 10 == 0)
            {
                _player.TakeDamage(-1);
            }

            _stats.Score += 10; // TODO: different values for diff packs
            NumberAccepted += 1;

            OnReceptacleChange.Invoke(CurrentAcceptedItem, NumberNeeded);
        }
        else
        {
            _player.TakeDamage(1);
        }

        Destroy(other.gameObject);
    }
}