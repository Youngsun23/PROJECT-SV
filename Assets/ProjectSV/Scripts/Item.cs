using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,

    Wood,
    Stone,
    Thread,
    Carrot,
    Stick,
    FishingPole,

    Else
}

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : GameDataBase
{
    public string Name => itemName;
    public string Info => itemInfo;
    public bool Stackable => stackable;
    public Sprite Icon => icon;
    public ToolAction OnToolAction => onToolAction;
    public ToolAction OnToolActionTileMap => onToolActionTileMap;
    public CropData CropData => crop;
    public ItemType Type => type;
    public bool Placeable => placeable;
    public GameObject ItemPrefab => itemPrefab;

    [SerializeField] private string itemName;
    [SerializeField] private string itemInfo;
    [SerializeField] private bool stackable;
    [SerializeField] private Sprite icon;
    [SerializeField] private ToolAction onToolAction;
    [SerializeField] private ToolAction onToolActionTileMap;
    [SerializeField] private CropData crop;
    [SerializeField] private ItemType type;
    [SerializeField] private bool placeable;
    [SerializeField] private GameObject itemPrefab;
}

[CreateAssetMenu(menuName = "Data/Item/Weapon")]
public class WeaponItem : Item
{
    public int Damage => damage;
    public float Radius => radius;

    [SerializeField] private int damage;
    [SerializeField] private float radius;
}