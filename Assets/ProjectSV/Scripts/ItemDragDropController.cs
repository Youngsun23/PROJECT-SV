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
    [SerializeField] private ItemSlot dragDropSlot;
    [SerializeField] private GameObject dragDropIcon;
    private RectTransform iconTransform;
    private Image dragDropSlotIcon;

    private void Start()
    {
        dragDropSlot = new ItemSlot();
        iconTransform = dragDropIcon.GetComponent<RectTransform>();
        dragDropSlotIcon = dragDropIcon.GetComponent<Image>();
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

    public void OnClick(ItemSlot inventorySlot)
    {
        if (dragDropSlot.Item == null)
        {
            dragDropSlot.Copy(inventorySlot);
            inventorySlot.Clear();
        }
        else
        {
            Item temp_item = inventorySlot.Item;
            int temp_count = dragDropSlot.Count;
            inventorySlot.Copy(dragDropSlot);
            dragDropSlot.Set(temp_item, temp_count);
        }
        UpdateIcon();
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
