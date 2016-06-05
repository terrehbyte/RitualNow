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

    [SerializeField]
    private SpriteRenderer desiredItemImage;

    private int NumberAccepted;
    public int NumberNeeded = 1;

    public UnityEvent OnReceptacleFull = new UnityEvent();

    void Scramble()
    {
        Debug.Log("Should scramble");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool validDrop = false;

        foreach(var type in AcceptedItems)
        {
            if (other.CompareTag(type.AcceptedTag))
            {
                _stats.ParcelPlacementCount += 1;

                if (_stats.ParcelPlacementCount % 10 == 0)
                {
                    _player.TakeDamage(-1);
                }

                validDrop = true;
                break;
            }
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