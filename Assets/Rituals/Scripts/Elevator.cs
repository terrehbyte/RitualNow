using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Elevator : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public float WaitTime = 1.0f;
    public float TimeToTravel = 1.0f;
    public bool Traveling
    {
        get; private set;
    }
    
    private bool AtStartPoint = true;

    private List<Rigidbody2D> passengers = new List<Rigidbody2D>();

    // TODO: add tons of settings like...
    //  - PreWarm (Start at start point)
    //  - PlaybackMode (PingPong, Loop, etc.)
    //  - Different ways to set waypoints (Absolute Position, Relative Position, Transform)
    
    public enum PlaybackMode
    {
        ONCE,
        RETURN
    }
    public PlaybackMode playbackSetting;

    public void Start()
    {
        // Start at starting position
        transform.position = StartPoint.position;
    }

    public void Toggle(bool returning = true)
    {
        if (Traveling)
            return;

        StartCoroutine(StartTraveling(AtStartPoint ? EndPoint.position : StartPoint.position, TimeToTravel, playbackSetting == PlaybackMode.RETURN && returning));
        AtStartPoint = !AtStartPoint;
    }

    IEnumerator StartTraveling(Vector3 goalPosition, float travelTime, bool shouldReturn = true)
    {
        
        //Debug.Assert(!Traveling, "Elevator started traveling while it was already traveling!");

        Vector3 startPosition = transform.position;
        float duration = 0.0f;
        float oldDuration = 0.0f;

        Traveling = true;
        while (duration < travelTime)
        {
            duration = Mathf.Clamp(duration + Time.fixedDeltaTime, 0f, travelTime);

            Vector3 newPosition = Vector3.Lerp(startPosition, goalPosition, duration / travelTime);

            Vector2 direction = (newPosition - transform.position).normalized;

            transform.position = newPosition;

            List<Rigidbody2D> rbodyRemoval = new List<Rigidbody2D>();

            foreach (var passenger in passengers)
            {
                try
                {
                    passenger.transform.Translate(direction * (duration - oldDuration));
                    //passenger.AddForce(direction * (duration - oldDuration), ForceMode2D.Force);
                }
                catch (NullReferenceException ex)
                {
                    rbodyRemoval.Add(passenger);
                    Debug.Log("Object was null" + ex.InnerException);
                }
            }

            foreach(var deathrow in rbodyRemoval)
            {
                passengers.Remove(deathrow);
            }

            oldDuration = duration;

            yield return new WaitForFixedUpdate();
        }

        Traveling = false;

        if (shouldReturn)
        {
            yield return new WaitForSeconds(WaitTime);

            Toggle(false);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D newRbody2D = collision.collider.GetComponent<Rigidbody2D>();

        if (newRbody2D != null && !newRbody2D.isKinematic)
        {
            if (!passengers.Contains(newRbody2D))
            {
                passengers.Add(newRbody2D);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D newRbody2D = collision.collider.GetComponent<Rigidbody2D>();

        if (newRbody2D != null && !newRbody2D.isKinematic)
        {
            if (passengers.Contains(newRbody2D))
            {
                passengers.Remove(newRbody2D);
            }
        }
    }
}
