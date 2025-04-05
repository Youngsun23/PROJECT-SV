using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item Item => item;
    public int Count => count;

    [SerializeField] private Item item;
    [SerializeField] private int count;

    public void Set(Item _item, int _count)
    {
        item = _item;
        count = _count;
    }

    public void SetItem(Item _item)
    {
        item = _item;
    }

    public void SetCount(int _count)
    {
        count = _count;
    }
    public void ChangeCount(int val)
    {
        count += val;
        if(count <= 0)
        {
            Clear();
        }
    }

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> ItemSlots => itemSlots;

    [SerializeField] private List<ItemSlot> itemSlots;

    public Action onChange;

    public void AddItem(Item item, int count = 1)
    {
        if (item.Stackable)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.Item == item);
            if(itemSlot != null)
            {
                itemSlot.ChangeCount(count);
            }
            else
            {
                itemSlot = itemSlots.Find(x => x.Item == null);
                if(itemSlot != null)
                {
                    itemSlot.SetItem(item);
                    itemSlot.SetCount(count);
                }
            }
        }
        else
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.Item == null);
            if(itemSlot != null)
            {
                itemSlot.SetItem(item);
                itemSlot.SetCount(count);
            }
        }

        onChange?.Invoke();
    }

    public void RemoveItem(Item item, int count = 1)
    {
        ItemSlot itemSlot = itemSlots.Find(x => x.Item == item);
        if (itemSlot == null)
            return;

        itemSlot.ChangeCount(-count);

        onChange?.Invoke();
    }
}
