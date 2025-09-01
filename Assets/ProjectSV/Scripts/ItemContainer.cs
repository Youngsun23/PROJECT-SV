using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    // ID
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

    public int CopyInHalf(ItemSlot slot)
    {
        item = slot.item;
        if (slot.count % 2 == 0)
        {
            count = slot.count / 2;
            return count;
        }
        else
        {
            count = slot.count / 2 + 1;
            return count - 1;
        }
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : GameDataBase
{
    public List<ItemSlot> ItemSlots => itemSlots;

    [SerializeField] private List<ItemSlot> itemSlots;

    public Action onChange;

    public void Init()
    {
        itemSlots = new List<ItemSlot>();   
        for(int i = 0; i < 30; i++)
        {
            itemSlots.Add(new ItemSlot());
        }
    }

    public void SetItemList(List<ItemSlot> list)
    {
        itemSlots = list;
    }

    public bool AddItem(Item item, int count = 1)
    {
        bool success = false;

        if (item.Stackable)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.Item == item);
            if(itemSlot != null)
            {
                itemSlot.ChangeCount(count);
                // ToDo: 최대 StackCount를 넘기는 경우, 최대치까지만 합치고, 나머지 개수만큼 드래그앤드랍슬롯에 남도록 처리 

                success = true;
            }
            else
            {
                itemSlot = itemSlots.Find(x => x.Item == null);
                if (itemSlot != null)
                {
                    itemSlot.SetItem(item);
                    itemSlot.SetCount(count);
                    success = true;
                }
                else
                {
                    Debug.Log("인벤토리가 꽉 찼습니다.");
                    success = false;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.Item == null);
            if (itemSlot != null)
            {
                itemSlot.SetItem(item);
                itemSlot.SetCount(count);
                success = true;
            }
            else
            {
                Debug.Log("인벤토리가 꽉 찼습니다.");
                success = false;
            }
        }

        onChange?.Invoke();
        return success;
    }

    public void RemoveItem(Item item, int count = 1)
    {
        ItemSlot itemSlot = itemSlots.Find(x => x.Item == item);
        if (itemSlot == null)
            return;

        itemSlot.ChangeCount(-count);

        onChange?.Invoke();
    }

    public void RemoveItemGrid()
    {
        foreach(var item in itemSlots)
        {
            if(item != null)
                item.ChangeCount(-1);
        }
    }

    public void ClearContainer()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].Clear();
        }
    }

    public bool IsFull()
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item == null)
                return false;
        }
        return true;
    }
}
