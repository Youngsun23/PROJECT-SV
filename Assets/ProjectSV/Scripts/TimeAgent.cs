using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void OnDestroy()
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
