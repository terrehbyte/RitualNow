using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
    public int count;
}

public class AssemblyLine : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public GameObject RailPrefab;

    public GameObject[] samples;

    public float SpawnInterval;
    private float SpawnTimer;

    public float Speed;

	// Update is called once per frame
	void Update ()
    {
        SpawnTimer += Time.deltaTime;

	    if (SpawnTimer >= SpawnInterval)
        {
            SpawnTimer -= SpawnInterval;

            var newRail = (Instantiate(RailPrefab, StartPoint.position, Quaternion.identity) as GameObject).GetComponent<Rail>();
            newRail.End = EndPoint;
            newRail.speed = Speed;

            var newProduct = (Instantiate(samples[Random.Range(0, samples.Length)], StartPoint.position + (Vector3.down / 2), Quaternion.identity) as GameObject );
            newProduct.GetComponent<SpringJoint2D>().connectedBody = newRail.GetComponent<Rigidbody2D>();


        }
        
	}
}
