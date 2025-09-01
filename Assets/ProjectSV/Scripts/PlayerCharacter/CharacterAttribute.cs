using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    public float MaxValue => DefaultValue + ModifierValue;
    public float CurrentValue => DefaultValue + ModifierValue + BuffedValue; /* - DecreaseValue*/

    public float DefaultValue { get; set; } // ĳ���� ���� ����
    public float ModifierValue { get; set; } // ����/�ӽ�, ����/�϶�, �ִ밪 ��ȭ�� (����, ���, ������, ��ų, ...)
    public float BuffedValue { get; set; } // �ӽ�, ����/�϶�, ���簪 ��ȭ�� (ü�� ���� �ƽ� �Ѿ�� ����)

    public System.Action<int, int> OnModifierChanged; // �ִ�, ����
    public System.Action<int, int> OnBuffedChanged; // �ִ�, ����
}

// M, B �򰥸��ϱ� ���� ��ġ �ٲٸ鼭 �����ϱ�