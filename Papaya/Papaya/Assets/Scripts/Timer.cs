using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 90;
    public Text timerText;
    void Update()
    {
        if(timeValue > 0)
        {
        timeValue -= Time.deltaTime;
        }
        else
    {
        timeValue = 0;
    }
    DisplayTime(timeValue);

    }
    void DisplayTime(float timeToDiplay)
    {
        if(timeToDiplay < 0)
        {
            timeToDiplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDiplay / 60);
        float seconds = Mathf.FloorToInt(timeToDiplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}" , minutes, seconds); 

    }
    
}