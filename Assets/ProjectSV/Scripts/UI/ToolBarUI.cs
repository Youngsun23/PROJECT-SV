using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUI : UIBase
{
    [SerializeField] private ToolBarPanel toolBarPanel;

    // Show, Hide �״��
    public override void Show()
    {
        base.Show();

        toolBarPanel.Show();
    }
}
