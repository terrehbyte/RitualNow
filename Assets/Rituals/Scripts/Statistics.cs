using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour
{
    public static Statistics instance;

    public int ParcelSpawnCount;
    public int ParcelPlacementCount;

    public float TimeElapsed;

    public bool ShouldReplaceInstance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && ShouldReplaceInstance)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	void Update()
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
