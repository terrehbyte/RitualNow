using UnityEngine;
using System.Collections;

using Zenject;

public class Rail : MonoBehaviour
{
    public float speed;

    public Transform End;

    void FixedUpdate()
    {
        Vector3 delta = ((End.position - transform.position).normalized) * (speed * Time.deltaTime);

        transform.position += delta;
    }

    public class Factory : GameObjectFactory<Rail>
    {
    }
}
