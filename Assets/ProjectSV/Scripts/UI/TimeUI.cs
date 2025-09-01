//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class TimeUI : UIBase
//{
//    [SerializeField] private TextMeshProUGUI textUI;

//    // Show, Hide ±×´ë·Î

//    private void Awake()
//    {
//        TimeManager.Singleton.OnTimeChanged += UpdateTimeUI;
//    }

//    private void OnDestroy()
//    {
//        TimeManager.Singleton.OnTimeChanged -= UpdateTimeUI;
//    }

//    public void UpdateTimeUI(int hour, int minute)
//    {
//        textUI.text = hour.ToString("00") + ":" + minute.ToString("00");
//    }
//}
