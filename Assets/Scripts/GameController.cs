using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;

    CameraFollow cameraController;

    private void Awake()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    void Start()
    {
        // Mengambil posisi checkpoint dari PlayerPrefs, jika belum ada maka menggunakan posisi awal
        checkpointPos = PlayerPrefs.HasKey("CheckpointPos") ? StringToVector2(PlayerPrefs.GetString("CheckpointPos")) : transform.position;
        playerRb = GetComponent<Rigidbody2D>();
        transform.position = checkpointPos; // Posisi awal player diatur ke checkpoint terakhir
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
        // Menyimpan posisi checkpoint ke PlayerPrefs
        PlayerPrefs.SetString("CheckpointPos", Vector2ToString(checkpointPos));
        PlayerPrefs.Save();
    }

    public void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }

    // Fungsi konversi Vector2 ke string
    private string Vector2ToString(Vector2 vector)
    {
        return vector.x + "," + vector.y;
    }

    // Fungsi konversi string ke Vector2
    private Vector2 StringToVector2(string str)
    {
        string[] parts = str.Split(',');
        float x = float.Parse(parts[0]);
        float y = float.Parse(parts[1]);
        return new Vector2(x, y);
    }
}
