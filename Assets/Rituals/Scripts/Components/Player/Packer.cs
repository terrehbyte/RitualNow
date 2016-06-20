using UnityEngine;
using System.Collections;
using System.Linq;

using Zenject;
using System;

namespace RitualWarehouse
{
    public class Packer : MonoBehaviour
    {
        [Inject]
        GameMode _game;

        [Inject]
        AssemblyLine _assemblyLine;

        [SerializeField]
        private int _livesCount = 3;
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

        private SpringJoint2D anchor;

        public Rigidbody2D picked
        {
            get
            {
                return _picked;
            }
            set
            {
                // HACK: this is ugly and bad and I should feel bad
                anchor.enabled = null != value;
                anchor.connectedBody = value;

                _picked = value;

                if (_picked != null)
                {
                    _picked.velocity = Vector2.zero;
                    pickedRBData = new RigidData(_picked);
                    _picked.gravityScale = 1f;
                }
            }
        }
        public float ThrowForceScalar = 0.5f;

        private Rigidbody2D _picked;
        private RigidData pickedRBData;

        private Vector2 oldPosition;
        private Vector2 newPosition;
        private Vector2 velocity2D
        {
            get
            {
                return (newPosition - oldPosition) / Time.fixedDeltaTime;
            }
        }

        public LayerMask PackerMask;

        [SerializeField]
        [Tooltip("Maximum distance between click location and parcel. Measured in units.")]
        private float PickerRadius = 2f; // TODO: should this go here?
                                         // TODO: This should be in addition to the radius of the object as a whole...

        [SerializeField]
        [Tooltip("Maximum distance between click location and the furthest point to an interactable collider. Measured in units.")]
        private float EverythingElseRadius = 0.5f;

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
            Collider2D hitCollider2D = Physics2D.OverlapPoint(transform.position, PackerMask);
            if (hitCollider2D != null && hitCollider2D.GetComponent<ItemIdentifier>())
            {
                return hitCollider2D.gameObject;
            }

            return null;
        }

        // Returns the nearest available parcel GameObject for a given screenspace position within a given radius in worldspace. If none, returns null.
        GameObject TryPickParcelCircle(Vector2 mousePosition, float radius)
        {
            Vector2 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!(_assemblyLine.Parcels.Count > 0))
                return null;

            var validParcelList = _assemblyLine.Parcels.Where(x => x != null).ToList();

            var closestParcel = validParcelList.OrderBy(x => Vector2.Distance(x.transform.position, mousePositionInWorldSpace)).First();
            float distance = Vector2.Distance(mousePositionInWorldSpace, closestParcel.transform.position);

            return distance < radius ? closestParcel : null;
        }

        // Returns whatever GameObject is directly underneath the given screenspace position. If none, returns null.
        GameObject TryPick(Vector2 mousePosition)
        {
            Collider2D hitCollider2D = Physics2D.OverlapPoint(transform.position, PackerMask);
            if (hitCollider2D != null)
            {
                return hitCollider2D.gameObject;
            }

            return null;
        }

        // Returns whatever GameObject is underneath the given screenspace position within a given radius in worldspace. If none, returns null.
        GameObject TryPickCircle(Vector2 mousePosition, float radius)
        {
            Collider2D hitCollider2D = Physics2D.OverlapCircle(transform.position, radius, PackerMask);
            if (hitCollider2D != null)
            {
                return hitCollider2D.gameObject;
            }

            return null;
        }

        void Start()
        {
            anchor = GetComponent<SpringJoint2D>();
            anchor.enabled = false;
        }

        void FixedUpdate()
        {
            oldPosition = newPosition;
            newPosition = transform.position;
        }

        void Update()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.back);

            // TODO: build input manager + support for multiple touches where possible
            if (Input.GetMouseButtonDown(0))
            {
                // Click Priority
                // 0. Whatever you literally clicked on?
                // 1. Parcels
                // 2. Everything Else 
                GameObject pickedObject = null;
                bool isParcel = false;

                // Try parcel (exact)
                if (pickedObject = TryPickParcel(transform.position))
                {
                    isParcel = true;
                }
                // Try anything (exact)
                else if(pickedObject = TryPick(transform.position))
                {

                }
                // Try parcel (lenient)
                else if(pickedObject = TryPickParcelCircle(transform.position, PickerRadius))
                {
                    isParcel = true;

                }
                // Try anything (lenient)
                else if(pickedObject = TryPickCircle(transform.position, EverythingElseRadius))
                {

                }

                if (isParcel)
                {
                    Rigidbody2D pickedRB = pickedObject.GetComponent<Rigidbody2D>();

                    if (pickedRB)
                        picked = pickedRB;
                }

                // Try interacting with whatever we managed to get.
                if (pickedObject != null)
                {                 
                    // try interaction
                    var interactables = pickedObject.GetComponents<IInteractable>();
                    foreach (var interaction in interactables)
                    {
                        interaction.OnInteract(this);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) && picked != null)   // let go
            {
                pickedRBData.Assign(picked);
                picked.velocity = velocity2D * ThrowForceScalar;
                picked = null;
            }
        }

        // Only for editor use.
        void Reset()
        {
            PackerMask = LayerMask.NameToLayer("Selectable");
        }
    }
}