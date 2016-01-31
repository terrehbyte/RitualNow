using UnityEngine;
using System.Collections;

public class FreeOnClick : MonoBehaviour {

    private SpringJoint2D spring;

    void Start()
    {
        spring = GetComponent<SpringJoint2D>();

        if (null == spring)
            Destroy(this);
    }

	void OnMouseDown()
    {
        Destroy(spring);
        Destroy(this);
    }
}
