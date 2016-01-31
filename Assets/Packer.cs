using UnityEngine;
using System.Collections;

public class Packer : MonoBehaviour
{
    public LayerMask PickerMask;

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

            _originalZLevel = value.transform.position.z;

            _picked = value;
        }
    }
    private Rigidbody2D _picked;
    private float _originalZLevel;

    void Start()
    {
        //Cursor.visible = false;

        anchor = GetComponent<SpringJoint2D>();
        anchor.enabled = false;
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            //Cursor.visible = false;

            RaycastHit hit = new RaycastHit();

            Debug.Log("Searching..");
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PickerMask))
            {
                Debug.LogWarning("HIt something!");
                var targetRbody = hit.collider.GetComponent<Rigidbody2D>();

                if (null != targetRbody)
                {
                    picked = targetRbody;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0) && picked != null)   // let go
        {
            picked = null;
        }
        else if (Input.GetMouseButton(0) && picked != null) // move to new position
        {
            picked.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.back * _originalZLevel);
        }

        //transform.position = ray.origin - Vector3.back * 2f;
    }



}
