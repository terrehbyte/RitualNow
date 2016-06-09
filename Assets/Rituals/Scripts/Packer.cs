using UnityEngine;
using System.Collections;
using System.Linq;

using Zenject;
using System;

public class Packer : MonoBehaviour
{
    [Inject]
    GameMode _game;

    [Inject]
    AssemblyLine _assemblyLine;

    public int LivesCount
    {
        get
        {
            return _livesCount;
        }
        private set
        {

            _livesCount = value;
        }
    }

    [SerializeField]
    private int _livesCount = 3;

    private SpringJoint2D anchor;

    public Rigidbody2D picked
    {
        get
        {
            return _picked;
        }
        set
        {
            anchor.enabled = null != value;
            anchor.connectedBody = value;
            
            _picked = value;

            if (_picked != null)
            {
                _picked.velocity = Vector2.zero;
                pickedRBData = new RigidData(_picked);
                _picked.gravityScale = 0f;
            }
        }
    }
    private Rigidbody2D _picked;
    private RigidData pickedRBData;

    [SerializeField]
    [Tooltip("Maximum distance between click location and parcel. Measured in units.")]
    private float PickerRadius = 2f; // TODO: should this go here?
    // TODO: This should be in addition to the radius of the object as a whole...

    void Start()
    {
        anchor = GetComponent<SpringJoint2D>();
        anchor.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (!_game.isGameActive)
        {
            return;
        }

        LivesCount -= damage;
    }

    GameObject TryPickParcel(Vector2 mousePosition)
    {
        Vector2 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!(_assemblyLine.Parcels.Count > 0))
            return null;

        var validParcelList = _assemblyLine.Parcels.Where(x => x != null).ToList();
        try
        {
            var closestParcel = validParcelList.OrderBy(x => Vector2.Distance(x.transform.position, mousePositionInWorldSpace)).First();
            float distance = Vector2.Distance(mousePositionInWorldSpace, closestParcel.transform.position);

            return distance < PickerRadius ? closestParcel : null;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return null;
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);
        //Cursor.visible = false;

        // TODO: build input manager + support for multiple touches where possible
        if (Input.GetMouseButtonDown(0))
        {
            GameObject pickedObject = TryPickParcel(Input.mousePosition);

            if (pickedObject != null)
            {
                var targetRbody = pickedObject.GetComponent<Rigidbody2D>();

                if (null != targetRbody)
                {
                    picked = targetRbody;
                }

                Interactor interactor = pickedObject.GetComponent<Interactor>();
                if (null != interactor)
                {
                    interactor.DoInteraction();
                }
            }

        }
        else if (Input.GetMouseButtonUp(0) && picked != null)   // let go
        {
            pickedRBData.Assign(picked);
            picked = null;
        }
        else if (Input.GetMouseButton(0) && picked != null) // move to new position
        {
            picked.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);
            
        }

        
    }

}
