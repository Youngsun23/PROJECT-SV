using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDDatePanel : TimeAgent
{
    // ½Ã°£ ÅØ½ºÆ® - 1Æ½¸¶´Ù 10ºÐ
    // ³¯¾¾ ¾ÆÀÌÄÜ - ÇÏ·ç(ÀÏ´Ü ³ÀµÖ)
    // °èÀý - ÇÑ´Þ(ÀÏ´Ü ³ÀµÖ)
    // ³¯Â¥ - ÇÏ·ç(ÀÏ´Ü ³ÀµÖ)
    [SerializeField] private TextMeshProUGUI timeTXT;
    [SerializeField] private Image weatherIcon;
    [SerializeField] private Image seasonIcon;
    [SerializeField] private TextMeshProUGUI dateTXT;
    private string dayOfTheWeek = "";

    protected override void Start()
    {
        base.Start();
        onTimeTick += Tick;
        TimeManager.Singleton.OnDateChanged += DateChange;

        Tick();
        DateChange(1); // ÀúÀåµÈ ³¯Â¥ °¡Á®¿Í¼­
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onTimeTick -= Tick;
        TimeManager.Singleton.OnDateChanged -= DateChange;
    }

    public void Tick()
    {
        timeTXT.text = TimeManager.Singleton.Hours.ToString() + ":" + TimeManager.Singleton.Minutes.ToString("00");
    }

    public void DateChange(int day)
    {
        switch(day % 7)
        {
            case 0:
                dayOfTheWeek = "SUN";
                break;
            case 1:
                dayOfTheWeek = "MON";
                break;
            case 3:
                dayOfTheWeek = "TUE";
                break;
            case 4:
                dayOfTheWeek = "WED";
                break;
            case 5:
                dayOfTheWeek = "THU";
                break;
            case 6:
                dayOfTheWeek = "FRI";
                break;
        }
        dateTXT.text = dayOfTheWeek + " " + day.ToString();
    }
}
