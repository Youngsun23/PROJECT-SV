using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI : UIBase
{
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
