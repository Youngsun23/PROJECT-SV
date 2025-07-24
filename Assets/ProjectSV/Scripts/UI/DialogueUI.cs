using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : UIBase
{
    public override void Show()
    {
        base.Show();

        UIManager.Hide<ToolBarUI>(UIType.ToolBar);

    }

    public override void Hide()
    {
        base.Hide();

        UIManager.Show<ToolBarUI>(UIType.ToolBar);
    }
}
