using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryUI : UIBase
{
    [SerializeField] private ChestInventoryPanel panel;

    public ChestInventoryPanel GetPanel() => panel;

    public override void Show()
    {
        base.Show();

        UIManager.Show<InventoryUI>(UIType.Inventory);
    }

    public override void Hide()
    {
        base.Hide();

        UIManager.Hide<InventoryUI>(UIType.Inventory);
    }
}
