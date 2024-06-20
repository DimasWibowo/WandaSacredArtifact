using UnityEngine;

public class TrapForm : MonoBehaviour
{
    public Transform startPoint; // Reference to the start point GameObject

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Make the platform static initially
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetPlayer(other.gameObject);
        }
    }

    private void ResetPlayer(GameObject player)
    {
        if (startPoint != null)
        {
            player.transform.position = startPoint.position; // Move player to the start point
        }
        else
        {
            Debug.LogWarning("Start point not set for TrapForm script.");
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero; // Reset player's velocity
        }
    }
}
