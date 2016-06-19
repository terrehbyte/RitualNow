using UnityEngine;

using System;
using System.Collections.Generic;

using Zenject;

namespace RitualWarehouse
{
    public class BoxerLine : ITickable
    {
        [Inject]
        Statistics _stats;

        [Inject]
        ItemDatabase ItemCatalog;

        [Serializable]
        public class Settings
        {
            public Transform SpawnPoint;
            public GameObject BoxPrefab;
        }

        public GameObject SpawnNewBox()
        {
            throw new NotImplementedException();

        }

        public void Tick()
        {

        }
    }
}