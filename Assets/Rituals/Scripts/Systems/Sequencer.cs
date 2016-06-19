using UnityEngine;
using System.Collections;

using Zenject;

namespace RitualWarehouse
{
    public class Sequencer : ITickable
    {
        [Inject]
        GameMode _game;

        [Inject]
        AssemblyLine _assem;

        [Inject]
        AssemblyLine.Settings _assemSettings;

        [Inject]
        Settings _settings;

        private float Accumulator;

        public void Tick()
        {
            if (!_game.isGameActive)
                return;

            if (_settings.MaxRamps <= 0)
                return;

            Accumulator += Time.deltaTime;

            if (Accumulator > _settings.Step)
            {
                // uncomment for hilarity
                Accumulator -= _settings.Step;

                RampUpProduction();
            }
        }

        public void RampUpProduction()
        {
            _settings.MaxRamps -= 1;

            Accumulator -= _settings.TimeBetweenRamp;

            _assem.addlDelay += _settings.TimeBetweenRamp;
            _assemSettings.SpawnInterval -= _settings.RampUpSpawnRate;
            _assemSettings.Speed += _settings.RampUpParcelSpeed;
        }

        [System.Serializable]
        public class Settings
        {
            public float Step = 15.0f;

            public int MaxRamps = 3;

            public float RampUpSpawnRate = 0.1f;
            public float RampUpParcelSpeed = 0.7f;

            public float TimeBetweenRamp = 7.0f;
        }
    }
}