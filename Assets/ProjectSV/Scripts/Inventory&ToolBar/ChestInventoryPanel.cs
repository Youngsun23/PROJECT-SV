using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryPanel : ItemPanel
{
    protected override ItemContainer Inventory => chestInventory; // global�� �ƴ϶� �� ItemContainer �����ڷ� �����ߵǴ°� �ƴ�? ����
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
