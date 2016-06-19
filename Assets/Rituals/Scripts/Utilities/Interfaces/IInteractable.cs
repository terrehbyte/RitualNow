using UnityEngine;
using System;
using System.Collections;

namespace RitualWarehouse
{
    public enum InteractEventState
    {
        BEGIN,
        CONTINUE,
        STOP
    }

    public class InteractArgs : EventArgs
    {
        public readonly InteractEventState InteractionType;

        public InteractArgs(InteractEventState desiredState)
        {
            InteractionType = desiredState;
        }
    }

    interface IInteractable
    {
        void OnInteract(object sender, InteractArgs args);
    }
}