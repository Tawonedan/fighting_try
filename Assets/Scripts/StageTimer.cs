using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class StageTimer : MonoBehaviour
{
    public TMP_Text timeText;


    float time;
    bool startTimer;


    // Start is called before the first frame update
    void Start()
    {
        time = 99f;
        timeText.text = time.ToString("0");
        startTimer = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;

        if (time > 0f)
        {
            time-= Time.deltaTime;
            timeText.text = time.ToString("0");
        }
    }

    void StopTimer()
    {
        
    }

}
