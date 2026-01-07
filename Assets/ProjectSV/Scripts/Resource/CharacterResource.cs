using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceTypes
{
    Coin,

    SpringOrb,
    SummerOrb,
    FallOrb,
    WinterOrb,

    EndField,
}

[Serializable]
public class CharacterResourceData
{
    public ResourceTypes Type {get; private set;}
    public int Count {get; private set;}

    public CharacterResourceData(ResourceTypes type, int count)
    {
        Type = type;
        Count = count;
    }
}

public class CharacterResource : MonoBehaviour
{
    public int Value { get; private set; }

    public System.Action<int> OnChanged;

    public void SetValue(int value)
    {
        Value = value;
    }
}
