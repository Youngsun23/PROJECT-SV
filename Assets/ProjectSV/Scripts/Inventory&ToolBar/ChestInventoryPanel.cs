using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryPanel : ItemPanel
{
    protected override ItemContainer Inventory => chestInventory; // global이 아니라 새 ItemContainer 생성자로 만들어야되는거 아님? ㅇㅇ
    [SerializeField] private ItemContainer chestInventory;

    public void SetChestInventory(ItemContainer inventory)
    {
        chestInventory = inventory;
    }

    public override void OnClick(int id, bool isLeft)
    {
        if (isLeft)
        {
            GameManager.Singleton.ItemDragDropController.OnLeftClick(Inventory.ItemSlots[id]);
        }
        else
            GameManager.Singleton.ItemDragDropController.OnRightClick(Inventory.ItemSlots[id]);

        Show();
    }
}
