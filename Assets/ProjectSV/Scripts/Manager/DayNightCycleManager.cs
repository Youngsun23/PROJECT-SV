using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycleManager : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private Color dayLightColor = Color.white;
    [SerializeField] private Color nightLightColor;
    [SerializeField] private AnimationCurve lightColorCurve;
    [SerializeField] private Light2D globalLight;

    [SerializeField] private float timeScale = 60f;
    private float time;
    private float Hours { get { return time / 3600f; } }
    private float Minutes { get { return time % 3600f / 60f; } }
    private int days;

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hour = (int)Hours;
        int minutes = (int)Minutes;
        textUI.text = hour.ToString("00") + ":" + minutes.ToString("00");
        float v = lightColorCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if(time > secondsInDay)
        {
            StartNextDay();
        }
    }

    private void StartNextDay()
    {
        time = 0;
        days++;
    }

}
