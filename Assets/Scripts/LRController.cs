// Import modul yang diperlukan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Buat kelas pemain
public class Pemain : MonoBehaviour
{
    // Variabel untuk menyimpan posisi pemain
    public Vector3 posisi;

    // Inisialisasi pemain
    void Start()
    {
        // Atur posisi pemain ke pusat layar
        posisi = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    // Update pemain setiap frame
    void Update()
    {
        // Periksa apakah tombol panah kiri atau kanan ditekan
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Gerakkan pemain ke kiri
            posisi.x -= 10;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Gerakkan pemain ke kanan
            posisi.x += 10;
        }
    }
}
