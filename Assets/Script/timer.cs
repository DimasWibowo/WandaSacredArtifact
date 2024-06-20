using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public GameObject Losepanel;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            TampilkanLosePanel();
        }
       
        int menit = Mathf.FloorToInt(remainingTime / 60);
        int detik = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
    }

    void TampilkanLosePanel()
    {
        Losepanel.SetActive(true);
        Time.timeScale = 0; // Menghentikan semua aktivitas di game
    }

}
