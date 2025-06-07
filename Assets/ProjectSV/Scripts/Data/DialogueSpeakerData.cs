using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// List로 한 곳에서 관리 안 하고 개별 SO로 할 이유가?
[CreateAssetMenu (menuName ="Data/Dialogue/Speaker")]
public class DialogueSpeakerData : GameDataBase
{
    public Sprite Portrait => portrait;
    public string Name => speakerName;

    [SerializeField] private Sprite portrait;
    [SerializeField] private string speakerName;
}
