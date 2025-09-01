using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item List")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<Item> items;
}
