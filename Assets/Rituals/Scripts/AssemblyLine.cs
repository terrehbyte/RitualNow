using UnityEngine;

using System;
using System.Collections.Generic;

using Zenject;

public class AssemblyLine : ITickable
{
    [Inject]
    Settings _settings;

    [Inject]
    Statistics _stats;

    private float SpawnTimer;

    public List<GameObject> Parcels
    {
        get
        {
            return _Parcels;
        }
        private set
        {
            _Parcels = value;
        }
    }
    private List<GameObject> _Parcels = new List<GameObject>();

    public float addlDelay;

    GameObject SpawnParcel( Item newItemType )
    {
        _stats.ParcelSpawnCount += 1;

        var newRail = GameObject.Instantiate(_settings.RailPrefab).GetComponent<Rail>();

        newRail.transform.position = _settings.StartPoint.position;
        newRail.End = _settings.EndPoint;
        newRail.speed = _settings.Speed;

        // HACK: can't call instantiate from in here, and it doesn't have any dependencies to inject?
        // HACK: I might just be dumb because instantiate might be a static method I can call from here
        var newParcel = newRail.AttachParcel(_settings.ParcelPrefab, newItemType);

        Parcels.Add(newParcel);

        return newRail.gameObject;
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
        for (int i = 0; i < Parcels.Count; ++i)
        {
            if (Parcels[i] != null)
            {
                GameObject.Destroy(Parcels[i]);
            }
        }

        Parcels.Clear();
    }

    [Serializable]
    public class Settings
    {
        public Transform StartPoint;
        public Transform EndPoint;

        public GameObject RailPrefab;
        public GameObject ParcelPrefab;

        public ItemDatabase ItemCatalog;

        public float Speed;             // TODO: Consider making this a setting for the rail
        public float SpawnInterval;
    }
}
