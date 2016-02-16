using UnityEngine;
using System.Collections;

using Zenject;
using System;

public class Statistics : ITickable, IInitializable
{
    [Obsolete("Use the instance provided via DI (Zenject)")]
    public static Statistics instance;

    public int ParcelSpawnCount;
    public int ParcelPlacementCount;

    public float TimeElapsed;

    public bool ShouldReplaceInstance;

    public void Initialize()
    {
        Debug.Log("Statistics initializing...");
        if (instance == null)
        {
            instance = this;
        }
    }

	public void Tick()
    {
        TimeElapsed += Time.deltaTime;
    }

    public void Reset()
    {
        ParcelSpawnCount = 0;
        ParcelPlacementCount = 0;

        TimeElapsed = 0;
    }
}
