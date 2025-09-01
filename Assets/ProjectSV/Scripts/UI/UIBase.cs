using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    PANEL_START,

    HUD,
    FadeInOut,

    PANEL_END,
    POPUP_START,

    Inventory,
    ToolBar,
    ToolTip,
    DragDrop,
    Dialogue,
    Time,
    TempButtons,
    // SkillTree_Temp,
    TempCraft,
    TempEquipment,
    Crafting,
    ChestInventory,

    POPUP_END,
}


public abstract class UIBase : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
