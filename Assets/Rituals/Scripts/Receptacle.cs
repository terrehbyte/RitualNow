using UnityEngine;
using System.Collections;

public class Receptacle : MonoBehaviour
{
    public string[] acceptedTypes;

    //public Color NormalColor = Color.white;
    //public Color HoverColor = Color.green;

    private SpriteRenderer spriteRen;

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
        foreach(var type in acceptedTypes)
        {
            if (other.CompareTag(type))
            {
                Debug.Log("Yay!");
                break;
            }
        }

        Destroy(other.gameObject);
    }

}
