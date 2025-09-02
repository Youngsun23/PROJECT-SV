using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeManager : SingletonBase<TimeManager>
{
    // 7:00AM ~ 1:00AM (하루 18시간)
    // 1틱 = 게임 10m = 현실 10s
    // 하루 = 108틱 = 현실 18m
    const float secondsInDay = 1500f; // 1AM~강제취침
    const float tickLength = 10f; // 현실 10s
    private float startTime = 420f; // 7AM

    // [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private Color dayLightColor = Color.white;
    [SerializeField] private Color nightLightColor;
    [SerializeField] private AnimationCurve lightColorCurve;
    [SerializeField] private Light2D globalLight;

    [SerializeField] private float timeScale = 1f;
    private float time;
    public int Hours => hours;
    public int Minutes => minutes;
    private int hours;
    private int minutes;
    private int days;

    private int prevHour = -1;
    private int prevMinute = -1;
    public event Action<int> OnDateChanged;

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
        days = 1;
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
        hours = (int)(time / (tickLength * 6f));
        minutes = (int)((time % (tickLength * 6f) / tickLength) * 10f);

        if (hours != prevHour || minutes != prevMinute)
        {
            prevHour = hours;
            prevMinute = minutes;
        }
    }

    //private void PhaseCalculation()
    //{
    //    return (int)(time / phaseLength) + (int)(days * phaseInDay);
    //}

    public void StartNextDay()
    {
        int skippedTicks = TimeSkipCalculation(time);
        SkipTick(skippedTicks);

        time = startTime;
        oldPhase = 0;
        days++;
        OnDateChanged?.Invoke(days);

        if (days >= 28)
            StartNextSeason();
    }

    private void StartNextSeason()
    {
        days = 1;

        // 
    }

    private int TimeSkipCalculation(float bedTime)
    {
        float skippedSeconds = 360 + (1500f - bedTime);
        int skippedTicks = (int)(skippedSeconds / tickLength);
        Debug.Log($"SkippedSeconds: {skippedSeconds} / SkippedTicks: {skippedTicks}");
        return skippedTicks;
    }

    public void SkipTick(int tick)
    {
        for (int i = 0; i < tick; i++)
        {
            for (int j = 0; j < agents.Count; j++)
            {
                agents[j]?.InvokeTick();
                // Debug.Log($"밤 중에 돌아간 틱 횟수: {skippedTicks} - 대상({agents.Count})");
            }
        }
    }
}
