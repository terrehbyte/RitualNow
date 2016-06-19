using UnityEngine;
using System.Collections;
namespace RitualWarehouse
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParcelState : MonoBehaviour
    {
        public enum ParcelStates
        {
            TRANSPORT,
            ACTIVE
        }

        [System.Serializable]
        public struct State
        {
            public Color StateColor;
            public Vector3 StateScale;

            public void Assign(Transform targetTransform, SpriteRenderer targetRenderer)
            {
                targetTransform.localScale = StateScale;
                targetRenderer.color = StateColor;
            }
        }

        private SpriteRenderer spriteRend;

        public State TransportState;
        public State ActiveState;

        private State _CurrentState;
        private State CurrentState
        {
            get
            {
                return _CurrentState;
            }
            set
            {
                _CurrentState = value;
                _CurrentState.Assign(transform, spriteRend);
            }
        }

        private void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();
            GetComponent<Interactor>().Interaction += OnInteract;
            CurrentState = TransportState;
        }

        private void OnInteract(object sender, InteractArgs args)
        {
            CurrentState = ActiveState;
        }
    }
}