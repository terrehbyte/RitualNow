using UnityEngine;
using System.Collections;

using Zenject;

public class Receptacle : MonoBehaviour
{
    [Inject]
    Statistics _stats;

    [Inject]
    Packer     _player;

    public string[] acceptedTypes;

    private SpriteRenderer spriteRen;

    [SerializeField]
    private SpriteRenderer desiredItemImage;

    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool validDrop = false;

        foreach(var type in acceptedTypes)
        {
            if (other.CompareTag(type))
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
        }

        Destroy(other.gameObject);
    }
}