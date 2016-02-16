using UnityEngine;
using System.Collections;

using Zenject;

public class Statistics : ITickable, IInitializable
{
    public static Statistics instance;

    public int ParcelSpawnCount;
    public int ParcelPlacementCount;

    public float TimeElapsed;

    public bool ShouldReplaceInstance;

    public void Initialize()
    {
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
