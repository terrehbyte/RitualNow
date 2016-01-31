using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public int count;
}

public class AssemblyLine : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public GameObject RailPrefab;
    public GameObject ParcelPrefab;

    public float SpawnInterval;
    private float SpawnTimer;

    public float Speed;

    public ItemDatabase curDatabase;

    private List<GameObject> parcels = new List<GameObject>();

    public float addlDelay;

    GameObject SpawnParcel( Item newItemType )
    {
        Statistics.instance.ParcelSpawnCount += 1;

        var newRail = (Instantiate(RailPrefab, StartPoint.position, Quaternion.identity) as GameObject).GetComponent<Rail>();
        newRail.End = EndPoint;
        newRail.speed = Speed;

        var newProduct = (Instantiate(ParcelPrefab, StartPoint.position + (Vector3.down / 2), Quaternion.identity) as GameObject);
        newProduct.GetComponent<SpringJoint2D>().connectedBody = newRail.GetComponent<Rigidbody2D>();
        newProduct.GetComponent<SpriteRenderer>().sprite = newItemType.image;
        newProduct.tag = newItemType.tag;

        parcels.Add(newProduct);

        return newRail.gameObject;
    }

	// Update is called once per frame
	void Update ()
    {
        

        if (addlDelay >= 0f)
        {
            addlDelay -= Time.deltaTime;
            return;
        }

        SpawnTimer += Time.deltaTime;

        if (SpawnTimer >= SpawnInterval)
        {
            SpawnTimer -= SpawnInterval;
            SpawnParcel(curDatabase.data[Random.Range(0, curDatabase.data.Length)]);
        }
        
	}

    public void ClearExisting()
    {
        for(int i = 0; i < parcels.Count; ++i)
        {
            if (parcels[i] != null)
            {
                Destroy(parcels[i]);
            }
        }

        parcels.Clear();
    }

}
