using UnityEngine;
using System.Collections;

using Zenject;

public class KillBox : MonoBehaviour {

    [Inject]
    Packer player;

	void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);

        if (coll.tag != "Untagged")
        {
            Debug.Log("DAM");
            player.TakeDamage(1);
        }
    }
}
