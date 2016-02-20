using UnityEngine;
using System.Collections;

using Zenject;

public class Receptacle : MonoBehaviour
{
    [Inject]
    Statistics _stats;

    public string[] acceptedTypes;

    //public Color NormalColor = Color.white;
    //public Color HoverColor = Color.green;

    private SpriteRenderer spriteRen;

    [SerializeField]
    private SpriteRenderer desiredItemImage;

    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();

        PickNewDesiredResult();
    }

    //void OnMouseOver()
    //{
    //    spriteRen.color = HoverColor;
    //}

    //void OnMouseExit()
    //{
    //    spriteRen.color = NormalColor;
    //}

    void PickNewDesiredResult()
    {

    }

    void Update()
    {

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
                    FindObjectOfType<Packer>().TakeDamage(-1);
                }

                validDrop = true;
                break;
            }
        }

        if (!validDrop)
        {
            FindObjectOfType<Packer>().TakeDamage(1);
        }

        Destroy(other.gameObject);
    }

}
