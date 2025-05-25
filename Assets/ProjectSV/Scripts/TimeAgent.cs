using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        TimeManager.Singleton.UnSubscribe(this);
    }

    public void Init()
    {
        TimeManager.Singleton.Subscribe(this);
    }

    public void InvokeTick()
    {
        onTimeTick?.Invoke();
    }
}
