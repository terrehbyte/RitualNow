using UnityEngine;
using System.Collections;

using Zenject;
using System;

public class Statistics : ITickable, IInitializable
{
    public int ParcelSpawnCount;
    public int ParcelPlacementCount;

    public float TimeElapsed;

    public int Score;

    public void Initialize()
    {
        Debug.Log("Statistics initializing...");
    }

	public void Tick()
    {
        TimeElapsed += Time.deltaTime;
    }

    public void Reset()
    {
        ParcelSpawnCount = 0;
        ParcelPlacementCount = 0;
        Score = 0;

        TimeElapsed = 0;
    }
}
