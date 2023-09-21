using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private float setTime;
    private float currentTime;

    private Color startColor;
    private Color targetColor; 

    [SerializeField]
    Image timerCircle;
    float elapsedTime = 0.0f;

    [SerializeField]
    Slider slider;


    private void Start()
    {
        startColor = Color.green;
        targetColor = Color.red;
        SetTime();   
    }
    private void Update()
    {
        RunTime();
        TransitionColor();
    }
    private void RunTime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("0");
            float ipValue = Mathf.Lerp(1, 0, elapsedTime/setTime);
            slider.value = ipValue;
        }
        else
        {
            //switch to highscore screen and bring forth score
        }
    }
    private void SetTime()
    {
        slider.value = 100;
        elapsedTime = 0.0f;
        currentTime = setTime;
    }

    private void TransitionColor()
    {
        elapsedTime += Time.deltaTime;
        Color currentColor = Color.Lerp(startColor, targetColor, elapsedTime/setTime);
        timerCircle.color = currentColor;
    }
}