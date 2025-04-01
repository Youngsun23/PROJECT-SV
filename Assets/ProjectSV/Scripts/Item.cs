using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name => itemName;
    public bool Stackable => stackable;
    public Sprite Icon => icon;
    public ToolAction OnToolAction => onToolAction;

    [SerializeField] private string itemName;
    [SerializeField] private bool stackable;
    [SerializeField] private Sprite icon;
    [SerializeField] private ToolAction onToolAction;
}
