using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    public float CurrentValue => DefaultValue + ModifierValue/* - DecreaseValue*/;

    public float DefaultValue { get; set; } // ĳ���� ���� ����
    public float ModifierValue { get; set; } // ������ �ӽõ�, ������ �϶��̵� ��ȭ��(����, ���, ������, ��ų, ...)
    // public float DecreaseValue { get; set; }

    public System.Action<float, float> OnChangedEvent;
    public System.Action<float> OnChangedBuffed;
}
