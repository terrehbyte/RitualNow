using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Interactor))]
public class FreeOnClick : MonoBehaviour, IInteractable
{
    private SpringJoint2D spring;

    void Start()
    {
        spring = GetComponent<SpringJoint2D>();

        if (null == spring)
            Destroy(this);

        GetComponent<Interactor>().Interaction += OnInteract;
    }

    void Release()
    {
        Destroy(spring);
        Destroy(this);
    }

    public void OnInteract(object sender, InteractArgs args)
    {
        Release();
    }
}
