using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class Clickable : MonoBehaviour, IInteractable
{
    public UnityEvent OnClick = new UnityEvent();

    public void OnInteract(object sender, InteractArgs args)
    {
        OnClick.Invoke();
    }

    public float ClickRadius = 1f;

    // HACK: need a proper InputManager
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(transform.position, mousePositionInWorldSpace) < ClickRadius)
            {
                OnInteract(this, new InteractArgs(InteractEventState.BEGIN));
            }
        }
    }
    void Start ()
    {
        GetComponent<Interactor>().Interaction += OnInteract;
	}
}
