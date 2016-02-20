using UnityEngine;
using System.Collections;

using Zenject;
using System;

public class GameMode : IInitializable
{
    [Inject]
    AssemblyLine _assem;

    [Inject]
    Statistics _stats;

    public bool isGameActive;
    public float timeToStart = 5.0f;

	// Use this for initialization
	public void Initialize ()
    {
        Debug.Log("GameMode Initializing...");
	}

    public void StartGame()
    {
        isGameActive = true;
        _assem.addlDelay += timeToStart;

        _stats.Reset();
        _assem.ClearExisting();
    }
}
