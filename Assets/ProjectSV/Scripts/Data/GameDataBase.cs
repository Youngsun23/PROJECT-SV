using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӵ����� ����-ID �ʼ� ... ���߿� �۾�
public abstract class GameDataBase : ScriptableObject
{
    [field: SerializeField] public string UniqueID { get; set; }
}
