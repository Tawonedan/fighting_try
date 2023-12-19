using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pemain : MonoBehaviour
{
    // Kecepatan pergerakan karakter
    public float kecepatanPergerakan = 5f;

    void Update()
    {
        float gerakanHorizontal = 0f;

        // Periksa hanya input dari keyboard
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gerakanHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gerakanHorizontal = 1f;
        }

        // Hitung pergerakan berdasarkan input horizontal
        Vector3 gerakan = new Vector3(gerakanHorizontal, 0f, 0f) * kecepatanPergerakan * Time.deltaTime;

        // Terapkan pergerakan pada posisi karakter
        transform.position += gerakan;
    }
}
