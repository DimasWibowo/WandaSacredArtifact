using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject door; // Referensi ke game object pintu

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang bersentuhan adalah pemain
        if (other.CompareTag("Player"))
        {
            // Menghancurkan pintu
            Destroy(door);

            // Menghancurkan objek kunci
            Destroy(gameObject);
        }
    }
}
