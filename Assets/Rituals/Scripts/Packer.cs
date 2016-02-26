using UnityEngine;
using System.Collections;

using Zenject;

public class Packer : MonoBehaviour
{
    [Inject]
    GameMode _game;

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

            if (_picked != null)
            {
                //Debug.Log(_picked.gameObject.name);
                pickedRBData = new RigidData(_picked);
                _picked.gravityScale = 0f;
            }
        }
    }
    private Rigidbody2D _picked;
    private RigidData pickedRBData;

    

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

    void Update()
    {
        //Cursor.visible = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            

            //Debug.Log("Searching..");
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, PickerMask);

            if (hit)
            {
                //Debug.LogWarning("Hit something!");
                var targetRbody = hit.collider.GetComponent<Rigidbody2D>();

                if (null != targetRbody)
                {
                    picked = targetRbody;
                }

                Interactor interactor = hit.collider.GetComponent<Interactor>();
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
            //Debug.Log(Input.mousePosition + Vector3.back * _originalZLevel);
            //picked.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);
            
        }

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);

        //transform.position = ray.origin - Vector3.back * 2f;
    }

}
