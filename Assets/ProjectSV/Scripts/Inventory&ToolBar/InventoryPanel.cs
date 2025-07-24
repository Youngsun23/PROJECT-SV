using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{

    public ToolTipPanel ToolTipPanel { get; private set; }

    protected override void Start()
    {
        base.Start();

        var toolTipUI = UIManager.Singleton.GetUI<ToolTipUI>(UIType.ToolTip);
        ToolTipPanel = toolTipUI.GetComponent<ToolTipPanel>();
    }

    private void OnDisable()
    {
        UIManager.Hide<ToolTipUI>(UIType.ToolTip);
    }

    public override void OnClick(int id, bool isLeft)
    {
        if (isLeft)
            GameManager.Singleton.ItemDragDropController.OnLeftClick(Inventory.ItemSlots[id]);
        else
            GameManager.Singleton.ItemDragDropController.OnRightClick(Inventory.ItemSlots[id]);

        Show();
    }
}
