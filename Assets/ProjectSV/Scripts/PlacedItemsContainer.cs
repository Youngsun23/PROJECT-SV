using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacedItem
{
    [SerializeField] private Item placedItem;
    [SerializeField] private Transform transform;
    [SerializeField] private Vector3Int positionOnGrid;

    public PlacedItem(Item _item, Transform _transform, Vector3Int _pos)
    {
        placedItem = _item; transform = _transform; positionOnGrid = _pos; 
    }
}


[CreateAssetMenu (menuName = "Data/Placed Items Container")]
public class PlacedItemsContainer : GameDataBase
{
    public List<PlacedItem> PlacedItems => placedItems;
    [SerializeField] private List<PlacedItem> placedItems;
}
