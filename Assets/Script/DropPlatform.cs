using System.Collections;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private bool isDropping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Make the platform static initially
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isDropping)
        {
            StartCoroutine(DropPlatform());
        }
    }

    private IEnumerator DropPlatform()
    {
        isDropping = true;
        yield return new WaitForSeconds(1);
        rb.isKinematic = false; // Allow the platform to fall
    }
}
