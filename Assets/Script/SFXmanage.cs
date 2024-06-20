using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps2D : MonoBehaviour
{
    public AudioClip walkingSFX; // Drag and drop the walking sound effect here in the inspector
    public float volume = 1.0f; // Volume of the walking sound
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private bool onGround;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        audioSource.clip = walkingSFX;
        audioSource.loop = true; // Loop the walking sound
        audioSource.volume = volume;
    }

    void Update()
    {
        if (onGround && Mathf.Abs(rb.velocity.x) > 0.1f && !audioSource.isPlaying)
        {
            // Player is moving and the sound is not playing, so play the sound
            audioSource.Play();
        }
        else if ((Mathf.Abs(rb.velocity.x) <= 0.1f || !onGround) && audioSource.isPlaying)
        {
            // Player is not moving or not grounded and the sound is playing, so stop the sound
            audioSource.Stop();
        }
    }

    // Ensure the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onGround = false;
        }
    }
}
