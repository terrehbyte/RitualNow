using UnityEngine;
using System.Collections;

public class Packer : MonoBehaviour
{
    public int LivesCount;

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

            _picked = value;

            if (_picked == null)
            {
                _originalZLevel = 0;
            }
            else
            {
                Debug.Log(_picked.gameObject.name);
                _originalZLevel = _picked.transform.position.z;

                _picked.gravityScale = 0f;
            }
        }
    }
    private Rigidbody2D _picked;
    private float _originalZLevel;

    void Start()
    {
        Cursor.visible = false;

        anchor = GetComponent<SpringJoint2D>();
        anchor.enabled = false;
    }


    void Update()
    {
        Cursor.visible = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            

            //Debug.Log("Searching..");
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, PickerMask);

            if (hit)
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
            _picked.gravityScale = 1f;
            picked = null;
        }
        else if (Input.GetMouseButton(0) && picked != null) // move to new position
        {
            //Debug.Log(Input.mousePosition + Vector3.back * _originalZLevel);
            //picked.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);
            
        }

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);

        //transform.position = ray.origin - Vector3.back * 2f;
    }



}
