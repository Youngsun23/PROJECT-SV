using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Dialogue/Script")]
public class DialogueData : GameDataBase
{
    public List<string> Script => script;
    [SerializeField] private List<string> script;

    public string SpeakerName => speaker.Name;
    public Sprite SpeakerPortrait => speaker.Portrait;
    [SerializeField] private DialogueSpeakerData speaker;
}

// Speaker 정보를 하나하나 연결? OMG