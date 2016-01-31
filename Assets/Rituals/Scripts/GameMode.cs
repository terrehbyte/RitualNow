using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;
    public AssemblyLine assem;

    public bool isGameActive;
    public float timeToStart = 5.0f;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
	}

    public void StartGame()
    {
        isGameActive = true;
        assem.addlDelay += timeToStart;

        Statistics.instance.Reset();
        assem.ClearExisting();
    }
}
