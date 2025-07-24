using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/CraftingRecipe")]
public class CraftingRecipe : GameDataBase
{
    public List<ItemSlot> Elements => elements;
    public ItemSlot Output => output;

    [SerializeField] private List<ItemSlot> elements;
    [SerializeField] private ItemSlot output;
}
