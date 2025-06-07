using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeManager : SingletonBase<TimeManager>
{
    const float secondsInDay = 86400f; // 24H
    const float tickLength = 900f; // 15m
    private float startTime = 28800f; // 8AM

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

    private List<TimeAgent> agents;
    private int oldPhase = 0; 

    protected override void Awake()
    {
        base.Awake();
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startTime;
    }

    public void Subscribe(TimeAgent agent)
    {
        agents.Add(agent);
    }

    public void UnSubscribe(TimeAgent agent)
    {
        agents.Remove(agent);
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        TimeCalculation();
        DayLight();

        if (time > secondsInDay)
        {
            StartNextDay();
        }

        TimeTick();
    }

    private void TimeTick()
    {
        int currentPhase = (int)(time / tickLength);

        if(oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i]?.InvokeTick();
            }
        }
    }

    private void DayLight()
    {
        float v = lightColorCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }

    private void TimeCalculation()
    {
        int hour = (int)Hours;
        int minutes = (int)Minutes;
        textUI.text = hour.ToString("00") + ":" + minutes.ToString("00");
    }

    private void StartNextDay()
    {
        time = 0;
        days++;
    }

}
