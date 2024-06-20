using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogInst : MonoBehaviour
{
    // Fields
    // Jendela
    public GameObject jendela;
    // Indikator
    public GameObject indikator;
    // Komponen teks
    public TMP_Text teksDialog;
    // Daftar dialog
    public List<string> daftarDialog;
    // Kecepatan menulis
    public float kecepatanMenulis;
    // Indeks dialog
    private int indeks;
    // Indeks karakter
    private int indeksKarakter;
    // Boolean mulai
    private bool mulai;
    // Tunggu boolean berikutnya
    private bool tungguBerikutnya;

    private void Awake()
    {
        ToggleIndikator(false);
        ToggleJendela(false);
    }

    private void ToggleJendela(bool tampil)
    {
        jendela.SetActive(tampil);
    }

    public void ToggleIndikator(bool tampil)
    {
        indikator.SetActive(tampil);
    }

    // Mulai Dialog
    public void MulaiDialog()
    {
        if (mulai)
            return;

        // Boolean untuk menunjukkan bahwa kita telah mulai
        mulai = true;
        // Tampilkan jendela
        ToggleJendela(true);
        // Sembunyikan indikator
        ToggleIndikator(false);
        // Mulai dengan dialog pertama
        DapatkanDialog(0);
    }

    private void DapatkanDialog(int i)
    {
        // Mulai indeks dari nol
        indeks = i;
        // Reset indeks karakter
        indeksKarakter = 0;
        // Bersihkan teks komponen dialog
        teksDialog.text = string.Empty;
        // Mulai menulis
        StartCoroutine(Menulis());
    }

    // Akhiri Dialog
    public void AkhiriDialog()
    {
        // Mulai dinonaktifkan
        mulai = false;
        // Nonaktifkan tunggu berikutnya juga
        tungguBerikutnya = false;
        // Hentikan semua Ienumerator
        StopAllCoroutines();
        // Sembunyikan jendela
        ToggleJendela(false);        
    }

    // Logika menulis
    IEnumerator Menulis()
    {
        yield return new WaitForSeconds(kecepatanMenulis);

        string dialogSekarang = daftarDialog[indeks];
        // Tulis karakter
        teksDialog.text += dialogSekarang[indeksKarakter];
        // Tingkatkan indeks karakter 
        indeksKarakter++;
        // Pastikan kita telah mencapai akhir kalimat
        if(indeksKarakter < dialogSekarang.Length)
        {
            // Tunggu x detik 
            yield return new WaitForSeconds(kecepatanMenulis);
            // Mulai ulang proses yang sama
            StartCoroutine(Menulis());
        }
        else
        {
            // Akhiri kalimat ini dan tunggu yang berikutnya
            tungguBerikutnya = true;
        }        
    }

    private void Update()
    {
        if (!mulai)
            return;

        if(tungguBerikutnya && Input.GetKeyDown(KeyCode.E))
        {
            tungguBerikutnya = false;
            indeks++;

            // Periksa apakah kita berada dalam cakupan daftar dialog
            if(indeks < daftarDialog.Count)
            {
                // Jika iya, ambil dialog berikutnya
                DapatkanDialog(indeks);
            }
            else
            {
                // Jika tidak, akhiri proses dialog
                ToggleIndikator(true);
                AkhiriDialog();
            }            
        }
    }
}
