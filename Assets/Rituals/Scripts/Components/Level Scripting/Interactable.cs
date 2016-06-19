using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace RitualWarehouse
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteraction = new UnityEvent();

        public void OnInteract(object sender)
        {
            OnInteraction.Invoke();
        }
    }
}