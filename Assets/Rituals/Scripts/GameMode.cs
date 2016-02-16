using UnityEngine;
using System.Collections;

using Zenject;

public class GameMode : IInitializable
{
    public static GameMode instance;
    public AssemblyLine assem;

    public bool isGameActive;
    public float timeToStart = 5.0f;

	// Use this for initialization
	public void Initialize ()
    {
        Debug.Log("GameMode Initializing...");

        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("You may have multiple GameMode objects!");
	}

    public void StartGame()
    {
        isGameActive = true;
        assem.addlDelay += timeToStart;

        Statistics.instance.Reset();
        assem.ClearExisting();
    }
}
