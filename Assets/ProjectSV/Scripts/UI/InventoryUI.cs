using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    // public GameObject dragDropIcon;

    public override void Show()
    {
        base.Show();

        UIManager.Hide<ToolBarUI>(UIType.ToolBar);
        // dragDropIcon.SetActive(false);
    }

    public override void Hide()
    {
        base.Hide();

        UIManager.Show<ToolBarUI>(UIType.ToolBar);
        // dragDropIcon.SetActive(false);
    }
}
