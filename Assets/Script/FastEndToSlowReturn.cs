using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastToEndSlowReturn : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speedToEnd = 100f; // Speed towards the end point
    public float speedToStart = 5f; // Speed towards the start point
    private Vector2 currentTarget;
    private float currentSpeed;

    private void Start()
    {
        currentTarget = endPoint.position;
        currentSpeed = speedToEnd;
    }

    private void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        platform.position = Vector2.MoveTowards(platform.position, currentTarget, currentSpeed * Time.deltaTime);

        if (Vector2.Distance(platform.position, currentTarget) <= 0.1f)
        {
            if (currentTarget == (Vector2)endPoint.position)
            {
                currentTarget = startPoint.position;
                currentSpeed = speedToStart;
            }
            else
            {
                currentTarget = endPoint.position;
                currentSpeed = speedToEnd;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(startPoint.position, endPoint.position);
            Gizmos.DrawSphere(startPoint.position, 0.1f);
            Gizmos.DrawSphere(endPoint.position, 0.1f);
        }
    }
}
