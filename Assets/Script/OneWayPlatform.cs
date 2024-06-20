using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private bool playerOnPlatform = false;

    void Update()
    {
        if (playerOnPlatform)
        {
            MoveToTarget(pointB.position);
        }
        else
        {
            MoveToTarget(pointA.position);
        }
    }

    void MoveToTarget(Vector3 target)
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    // Detect when the player steps onto the platform
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    // Detect when the player steps off the platform
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}