using UnityEngine;
using System.Collections;
using System;

public class FreeOnClick : MonoBehaviour, IInteractable
{
    private SpringJoint2D spring;

    void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        Debug.Assert(spring, "FreeOnClick needs a spring to destroy!");

        var interactor = GetComponent<Interactor>();
        if(interactor != null)
        {
            interactor.Interaction += OnInteract;
        }
    }

    public void Release()
    {
        Destroy(spring);
        Destroy(this);
    }

    public void OnInteract(object sender, InteractArgs args)
    {
        Release();
    }
}
