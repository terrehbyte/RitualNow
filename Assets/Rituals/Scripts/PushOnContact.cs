using UnityEngine;
using System.Collections;

public class PushOnContact : MonoBehaviour
{
    [SerializeField]
    private float PushForce;
    private Rigidbody2D rbody2D;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

	void OnCollisionStay2D(Collision2D incident2D)
    {
        var otherRbody2D = incident2D.collider.GetComponent<Rigidbody2D>();

        if (otherRbody2D != null)
        {
            //otherRbody2D.AddForceAtPosition(incident2D.contacts[0].point, transform)
        }
    }
}
