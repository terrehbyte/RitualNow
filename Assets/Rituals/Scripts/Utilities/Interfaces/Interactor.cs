using UnityEngine;
using System.Collections;
using System;

public class Interactor : MonoBehaviour
{
    public event EventHandler<InteractArgs> Interaction;

    public void DoInteraction()
    {
        Interaction.Raise(this, new InteractArgs(InteractEventState.BEGIN));
    }
}
