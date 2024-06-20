using UnityEngine;

public class FollowPlayerOnTrigger : MonoBehaviour
{
    // Tag untuk player
    GameObject Player;

    // Kecepatan mengikuti (opsional, jika tidak ingin langsung menjadi anak)
    public float followSpeed = 5f;

    // Jarak yang diinginkan antara objek dan player
    public float followDistance = 2f;

    // Referensi ke player
    private Transform playerTransform;

    // Flag untuk mengetahui apakah objek harus mengikuti player
    private bool shouldFollow = false;

    void Start()
    {
        // Inisialisasi variabel jika perlu
    }

    void Update()
    {
        if (shouldFollow && playerTransform != null)
        {
            // Hitung jarak antara objek dan player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Jika jarak lebih besar dari yang diinginkan, maka bergeraklah mendekati player
            if (distance > followDistance)
            {
                Vector3 targetPosition = playerTransform.position - direction * followDistance;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Periksa apakah yang menyentuh adalah player
        if (other.CompareTag("Player"))
        {
            // Set playerTransform dan aktifkan flag follow
            playerTransform = other.transform;
            shouldFollow = true;
        }
    }
}
