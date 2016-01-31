using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

    public Packer player;

	void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);

        if (coll.tag != "Untagged")
        {
            player.LivesCount -= 1;
        }
    }
}
