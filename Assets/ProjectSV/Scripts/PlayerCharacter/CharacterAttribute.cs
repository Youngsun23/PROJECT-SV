using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    public float MaxValue => DefaultValue + ModifierValue;
    public float CurrentValue => DefaultValue + ModifierValue + BuffedValue; /* - DecreaseValue*/

    public float DefaultValue { get; set; } // 캐릭터 태초 상태
    public float ModifierValue { get; set; } // 영구/임시, 증가/하락, 최대값 변화값 (레벨, 장비, 아이템, 스킬, ...)
    public float BuffedValue { get; set; } // 임시, 증가/하락, 현재값 변화값 (체력 제외 맥스 넘어서기 가능)

    public System.Action<int, int> OnModifierChanged; // 최대, 현재
    public System.Action<int, int> OnBuffedChanged; // 최대, 현재
}

// M, B 헷갈리니까 실제 수치 바꾸면서 구분하기