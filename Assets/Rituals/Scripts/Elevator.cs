using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public float TimeToTravel = 1.0f;
    public bool Traveling
    {
        get; private set;
    }
    
    private bool AtStartPoint = true;

    // TODO: add tons of settings like...
    //  - PreWarm (Start at start point)
    //  - PlaybackMode (PingPong, Loop, etc.)
    //  - Different ways to set waypoints (Absolute Position, Relative Position, Transform)
    
    public void Start()
    {
        // Start at starting position
        transform.position = StartPoint.position;
    }

    public void Toggle()
    {
        StartCoroutine(StartTraveling(AtStartPoint ? EndPoint.position : StartPoint.position, TimeToTravel));
        AtStartPoint = !AtStartPoint;
    } 

    IEnumerator StartTraveling(Vector3 goalPosition, float travelTime)
    {
        Debug.Assert(!Traveling, "Elevator started traveling while it was already traveling!");

        Vector3 startPosition = transform.position;
        float duration = 0.0f;

        Traveling = true;
        while (duration < travelTime)
        {
            duration = Mathf.Max(duration + Time.fixedDeltaTime, travelTime);

            transform.position = Vector3.Lerp(startPosition, goalPosition, duration / travelTime);

            yield return new WaitForFixedUpdate();
        }
        Traveling = false;
    }
}
