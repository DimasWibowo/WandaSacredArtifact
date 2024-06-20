using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class TriggerTime : MonoBehaviour
{
    public PlayableDirector playableDirector; // Referensi ke PlayableDirector
    public float playTime = 5.0f; // Durasi dalam detik setelah Timeline dimatikan
    public GameObject targetGameObject; // GameObject yang akan diaktifkan dan dinonaktifkan
    public Collider2D triggerCollider; // Collider yang akan dinonaktifkan

    void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan hanya memicu jika player yang masuk
        if (other.CompareTag("Player"))
        {
            // Aktifkan GameObject
            targetGameObject.SetActive(true);
            // Mulai memainkan Timeline
            playableDirector.Play();
            // Mulai Coroutine untuk menghentikan Timeline dan menonaktifkan GameObject serta Collider setelah waktu tertentu
            StartCoroutine(StopTimelineAndDeactivateGameObject(playTime));
        }
    }

    IEnumerator StopTimelineAndDeactivateGameObject(float time)
    {
        // Tunggu selama durasi yang ditentukan
        yield return new WaitForSeconds(time);
        // Hentikan Timeline
        playableDirector.Stop();
        // Nonaktifkan GameObject
        targetGameObject.SetActive(false);
        // Nonaktifkan Collider
        triggerCollider.enabled = false;
    }
}
