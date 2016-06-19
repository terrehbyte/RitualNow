using UnityEngine;
using System;
using System.Collections;

namespace RitualWarehouse
{
    interface IInteractable
    {
        void OnInteract(object sender);
    }
}