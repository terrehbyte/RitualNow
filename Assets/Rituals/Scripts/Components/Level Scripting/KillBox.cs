using UnityEngine;
using System.Collections;

using Zenject;

namespace RitualWarehouse
{
    public class KillBox : MonoBehaviour
    {

        [Inject]
        Packer player;

        void OnTriggerEnter2D(Collider2D coll)
        {
            Destroy(coll.gameObject);

            if (coll.tag != "Untagged")
            {
                player.TakeDamage(1);
            }
        }
    }
}