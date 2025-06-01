using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id, bool isLeft)
    {
        if (isLeft)
            GameManager.Singleton.ItemDragDropController.OnLeftClick(Inventory.ItemSlots[id]);
        else
            GameManager.Singleton.ItemDragDropController.OnRightClick(Inventory.ItemSlots[id]);

        Show();
    }
}
