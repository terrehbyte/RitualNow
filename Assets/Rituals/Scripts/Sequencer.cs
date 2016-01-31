using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour
{
    private float Accumulator;
    public float Step = 15.0f;

    public int MaxRamps = 3;

    public AssemblyLine assem;
    public float RampUpSpawnRate = 0.1f;
    public float RampUpParcelSpeed = 0.7f;

    public float TimeBetweenRamp = 7.0f;

    void Update()
    {
        if (!GameMode.instance.isGameActive)
            return;

        if (MaxRamps <= 0)
            return;

        Accumulator += Time.deltaTime;

        if (Accumulator > Step)
        {
            // uncomment for hilarity
            Accumulator -= Step;

            RampUpProduction();
        }
    }

    public void RampUpProduction()
    {
        Debug.Log("Work harder!");

        MaxRamps -= 1;

        Accumulator -= TimeBetweenRamp;
        assem.addlDelay += TimeBetweenRamp;

        assem.SpawnInterval -= RampUpSpawnRate;
        assem.Speed += RampUpParcelSpeed;
    }
}
