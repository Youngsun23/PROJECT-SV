using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacedItem
{
    public Item Item => placedItem;
    public Transform Transform => transform;    
    public Vector3Int Position => positionOnGrid;
    // public string ObjectState => objectState;
    public ItemConvertorData ConvertorData => data;

    [SerializeField] private Item placedItem;
    [SerializeField] private Transform transform;
    [SerializeField] private Vector3Int positionOnGrid;
    [SerializeField] private ItemConvertorData data;

    ///// <summary>
    ///// Serialized JSON string which contains the state of the object
    ///// </summary>
    //[SerializeField] private string objectState;

    public PlacedItem(Item _item, Vector3Int _pos)
    {
        placedItem = _item; positionOnGrid = _pos; data = null;
    }

    public void SetTransform(Transform _transform) { transform = _transform; } 
    // public void SetObjectState(string _string) { objectState = _string; }
    public void SetItemConvertorData(ItemConvertorData _data) { data = _data; }
}

[Serializable]
public class ItemConvertorData
{
    public ItemSlot ItemSlot => itemSlot;
    public float Timer => timer;

    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private float timer;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
        timer = 0;
    }

    public void SetTImer(float time)
    {
        timer = time;
    }

    public void TickTimer(float time)
    {
        timer += time;
    }
}

[CreateAssetMenu (menuName = "Data/Placed Items Container")]
public class PlacedItemsContainer : GameDataBase
{
    public List<PlacedItem> PlacedItems => placedItems;
    [SerializeField] private List<PlacedItem> placedItems;

    public void SetPlacedItemsList(List<PlacedItem> placedItems)
    {
        this.placedItems = placedItems;
    }

    public PlacedItem GetPlacedItem(Vector3Int pos)
    {
        return placedItems.Find(x => x.Position == pos);
    }

    public void ClearPlacedItemsList()
    {
        placedItems.Clear();
    }
}
