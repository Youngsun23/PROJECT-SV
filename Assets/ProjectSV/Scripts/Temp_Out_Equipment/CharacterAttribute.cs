using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    public float CurrentValue => DefaultValue + ModifierValue/* - DecreaseValue*/;

    public float DefaultValue { get; set; } // 캐릭터 태초 상태
    public float ModifierValue { get; set; } // 영구든 임시든, 증가든 하락이든 변화값(레벨, 장비, 아이템, 스킬, ...)
    // public float DecreaseValue { get; set; }

    public System.Action<float, float> OnChangedEvent;
    public System.Action<float> OnChangedBuffed;
}
