using UnityEngine;
using System.Collections;
using System;

using RitualWarehouse.Extensions;

namespace RitualWarehouse
{
    public class Interactor : MonoBehaviour
    {
        public event EventHandler<InteractArgs> Interaction;

        public void DoInteraction()
        {
            Interaction.Raise(this, new InteractArgs(InteractEventState.BEGIN));
        }
    }

}