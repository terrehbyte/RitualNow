using UnityEngine;
using System.Collections;

using Zenject;

public class Sequencer : ITickable
{ 
    [Inject]
    GameMode _game;

    [Inject]
    AssemblyLine _assem;

    [Inject]
    AssemblyLine.Settings _assemSettings;

    private float Accumulator;
    public float Step = 15.0f;

    public int MaxRamps = 3;

    public AssemblyLine assem;
    public float RampUpSpawnRate = 0.1f;
    public float RampUpParcelSpeed = 0.7f;

    public float TimeBetweenRamp = 7.0f;

    public void Tick()
    {
        if (!_game.isGameActive)
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

        _assem.addlDelay += TimeBetweenRamp;
        _assemSettings.SpawnInterval -= RampUpSpawnRate;
        _assemSettings.Speed += RampUpParcelSpeed;
    }
}
