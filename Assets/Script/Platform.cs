using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    BoxCollider2D collider;

    void Start()
    {
       collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        plat();
    }

    void plat()
    {
        if (Player.transform.position.y < transform.position.y)
        {
            collider.enabled = false;
        }
        if (Player.transform.position.y > transform.position.y)
        {
            collider.enabled = true;
        }
    }
}
