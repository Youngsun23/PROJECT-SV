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
    public ItemConvertorData ConvertorData => convertorData;
    public ChestStorageData StorageData => storageData;

    [SerializeField] private Item placedItem;
    [SerializeField] private Transform transform;
    [SerializeField] private Vector3Int positionOnGrid;
    [SerializeField] private ItemConvertorData convertorData;
    [SerializeField] private ChestStorageData storageData;

    ///// <summary>
    ///// Serialized JSON string which contains the state of the object
    ///// </summary>
    //[SerializeField] private string objectState;

    public PlacedItem(Item _item, Vector3Int _pos)
    {
        placedItem = _item; positionOnGrid = _pos; convertorData = null; storageData = null;
    }

    public void SetTransform(Transform _transform) { transform = _transform; } 
    // public void SetObjectState(string _string) { objectState = _string; }
    public void SetItemConvertorData(ItemConvertorData _data) { convertorData = _data; }
    public void SetChestStorageData(ChestStorageData _data) { storageData = _data; }
}

[Serializable]
public class ItemConvertorData
{
    public ItemSlot ItemSlot => itemSlot;
    public float CurrentTimer => currentConvertingTimer;
    public float FullTimer => fullConvertingTimer;
    public bool IsConvertingOver => isConvertingOver;
    public bool IsConverting => isConverting;

    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private float currentConvertingTimer;
    [SerializeField] private float fullConvertingTimer;
    [SerializeField] private bool isConvertingOver = false;
    [SerializeField] private bool isConverting = false;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
        currentConvertingTimer = 0;
        fullConvertingTimer = 5;
    }

    public void SetFullTimer(float time)
    {
        fullConvertingTimer = time;
    }

    public void TickConvertingTimer(float tick)
    {
        currentConvertingTimer += tick;
    }

    public void SetIsConvertingOver(bool tf)
    {
        isConvertingOver = tf;
    }

    public void SetIsConverting(bool tf)
    {
        isConverting = tf;
    }

    public void ResetData()
    {
        itemSlot.Clear();
        currentConvertingTimer = 0;
        isConvertingOver = false;
        isConverting = false;
    }
}

[Serializable]
public class ChestStorageData
{
    public List<ItemSlot> ItemSlots => itemSlots;
    [SerializeField] private List<ItemSlot> itemSlots;

    public ChestStorageData()
    {
        itemSlots = new List<ItemSlot>();
        for(int i = 0; i < 30; i++)
        {
            itemSlots.Add(new ItemSlot());
        }
    }

    public void SetData(List<ItemSlot> container)
    {
        itemSlots = container;
    }

    public void ResetData()
    {
        itemSlots.Clear();
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
