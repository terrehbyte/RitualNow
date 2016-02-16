using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Zenject;
using System;

[System.Serializable]
public class Wave
{
    public int count;
}

public class AssemblyLine : ITickable
{
    [Inject]
    Settings _settings;

    private float SpawnTimer;

    public float Speed;

    private List<GameObject> parcels = new List<GameObject>();

    public float addlDelay;

    GameObject SpawnParcel( Item newItemType )
    {
        //Statistics.instance.ParcelSpawnCount += 1;

        //var newRail = (Instantiate(RailPrefab, StartPoint.position, Quaternion.identity) as GameObject).GetComponent<Rail>();
        //newRail.End = EndPoint;
        //newRail.speed = Speed;

        //var newProduct = (Instantiate(ParcelPrefab, StartPoint.position + (Vector3.down / 2), Quaternion.identity) as GameObject);
        //newProduct.GetComponent<SpringJoint2D>().connectedBody = newRail.GetComponent<Rigidbody2D>();
        //newProduct.GetComponent<SpriteRenderer>().sprite = newItemType.image;
        //newProduct.tag = newItemType.tag;

        //parcels.Add(newProduct);

        //return newRail.gameObject;
        Debug.Log("Spawn Parcel");

        return null;
    }

	// Update is called once per frame
	public void Tick ()
    {
        if (addlDelay >= 0f)
        {
            addlDelay -= Time.deltaTime;
            return;
        }

        SpawnTimer += Time.deltaTime;

        if (SpawnTimer >= _settings.SpawnInterval)
        {
            SpawnTimer -= _settings.SpawnInterval;
            SpawnParcel(_settings.ItemCatalog.data[UnityEngine.Random.Range(0, _settings.ItemCatalog.data.Length)]);
        }
        
	}

    public void ClearExisting()
    {
        //for(int i = 0; i < parcels.Count; ++i)
        //{
        //    if (parcels[i] != null)
        //    {
        //        Destroy(parcels[i]);
        //    }
        //}

        //parcels.Clear();
    }

    [Serializable]
    public class Settings
    {
        public Transform StartPoint;
        public Transform EndPoint;

        public GameObject RailPrefab;
        public GameObject ParcelPrefab;

        public ItemDatabase ItemCatalog;

        public float SpawnInterval;
    }
}
