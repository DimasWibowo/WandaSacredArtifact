using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PemicuInstruksi : MonoBehaviour
{
    public DialogInst dialogScript; // Pastikan tipe dialogScript sesuai dengan kelas yang tepat
    private bool pemainTerdeteksi;

    // Deteksi trigger dengan pemain
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika kita mendeteksi pemain, aktifkan pemainTerdeteksi dan tampilkan indikator
        if(collision.tag == "Player")
        {
            pemainTerdeteksi = true;
            dialogScript.ToggleIndikator(pemainTerdeteksi);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Jika kita kehilangan trigger dengan pemain, nonaktifkan pemainTerdeteksi dan sembunyikan indikator
        if (collision.tag == "Player")
        {
            pemainTerdeteksi = false;
            dialogScript.ToggleIndikator(pemainTerdeteksi);
            dialogScript.AkhiriDialog();
        }
    }

    // Sementara terdeteksi jika kita berinteraksi mulai dialog
    private void Update()
    {
        if(pemainTerdeteksi && Input.GetKeyDown(KeyCode.E))
        {
            dialogScript.MulaiDialog();
        }
    }
}
