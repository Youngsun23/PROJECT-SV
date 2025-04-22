using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임데이터 묶기-ID 필수 ... 나중에 작업
public abstract class GameDataBase : ScriptableObject
{
    [field: SerializeField] public string UniqueID { get; set; }
}
