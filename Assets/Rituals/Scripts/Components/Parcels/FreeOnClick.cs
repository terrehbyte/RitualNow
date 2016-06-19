using UnityEngine;
using System.Collections;
using System;
namespace RitualWarehouse
{
    public class FreeOnClick : MonoBehaviour, IInteractable
    {
        private SpringJoint2D spring;

        void Start()
        {
            spring = GetComponent<SpringJoint2D>();
            Debug.Assert(spring, "FreeOnClick needs a spring to destroy!");
        }

        public void Release()
        {
            Destroy(spring);
            Destroy(this);
        }

        public void OnInteract(object sender)
        {
            Release();
        }
    }
}