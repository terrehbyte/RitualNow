using UnityEngine;
using System.Collections;

using Zenject;

public class Rail : MonoBehaviour
{
    public float speed;

    public Transform End;

    void FixedUpdate()
    {
        Vector3 delta = ((End.position - transform.position).normalized) * (speed * Time.deltaTime);

        transform.position += delta;
    }

    public GameObject AttachParcel(GameObject parcelPrefab, Item itemType )
    {
        var newProduct = (Instantiate(parcelPrefab, transform.position + (Vector3.down / 2), Quaternion.identity) as GameObject);
        newProduct.GetComponent<SpringJoint2D>().connectedBody = transform.GetComponent<Rigidbody2D>();
        newProduct.GetComponent<SpriteRenderer>().sprite = itemType.image;
        newProduct.tag = itemType.tag;

        return newProduct;
    }

    public class Factory : GameObjectFactory<Rail>
    {
    }
}
