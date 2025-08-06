using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemDragDropController : MonoBehaviour
{
    public ItemSlot DragDropSlot => dragDropSlot;
    [SerializeField] private ItemSlot dragDropSlot;
    [SerializeField] private GameObject dragDropIcon;
    private RectTransform iconTransform;
    private Image dragDropSlotIcon;

    private void Start()
    {
        dragDropSlot = new ItemSlot();
        iconTransform = dragDropIcon.GetComponent<RectTransform>();
        dragDropSlotIcon = dragDropIcon.GetComponent<Image>();
        UpdateIcon();
    }

    private void Update()
    {
        if(dragDropIcon.activeInHierarchy)
        {
            iconTransform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if(!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    worldPosition.z = 0;
                    ItemSpawnManager.Singleton.SpawnItem(worldPosition, dragDropSlot.Item, dragDropSlot.Count);

                    dragDropSlot.Clear();
                    dragDropIcon.SetActive(false);
                }
            }
        }
    }

    public bool ConvertableCheck(Item item, int count = 1)
    {
        if (dragDropSlot == null) return false;

        if(item.Stackable)
        {
            return dragDropSlot.Item == item && dragDropSlot.Count >= count;
        }
        return dragDropSlot.Item == item;
    }

    public void RemoveItem(int count = 1)
    {
        if(dragDropSlot.Item.Stackable)
        {
            dragDropSlot.ChangeCount(-count);
            if (dragDropSlot.Count <= 0)
                dragDropSlot.Clear();
        }
        else
            dragDropSlot.Clear();

        UpdateIcon();
    }

    public void OnLeftClick(ItemSlot inventorySlot)
    {
        //if(inventorySlot == )

        if (dragDropSlot.Item == null)
        {
            dragDropSlot.Copy(inventorySlot);
            inventorySlot.Clear();
        }
        else
        {
            // 다른 아이템이면 바꿔치기, 같은 아이템이면 합치기?
            Item temp_item = inventorySlot.Item;
            int temp_count = inventorySlot.Count;
            if(temp_item != dragDropSlot.Item)
            {
                inventorySlot.Copy(dragDropSlot);
                dragDropSlot.Set(temp_item, temp_count);
            }
            else
            {
                inventorySlot.Set(temp_item, temp_count + dragDropSlot.Count);
                dragDropSlot.Clear();
            }
        }
        UpdateIcon();
    }

    // 우클릭 -> 절반만 선택
    public void OnRightClick(ItemSlot inventorySlot)
    {
        if(dragDropSlot.Item != null || inventorySlot.Count == 1)
        {
            OnLeftClick(inventorySlot);
        }
        else
        {
            int remain = dragDropSlot.CopyInHalf(inventorySlot);
            inventorySlot.Set(inventorySlot.Item, remain);

            UpdateIcon();
        }
    }

    private void UpdateIcon()
    {
        if (dragDropSlot.Item == null)
        {
            dragDropIcon.SetActive(false);
        }
        else
        {
            dragDropSlotIcon.sprite = dragDropSlot.Item.Icon;
            dragDropIcon.SetActive(true);
        }
    }
}
