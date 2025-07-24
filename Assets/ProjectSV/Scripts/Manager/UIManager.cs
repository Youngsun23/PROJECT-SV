using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    public static T Show<T>(UIType uiType) where T : UIBase
    {
        var newUI = Singleton.GetUI<T>(uiType);
        if (newUI == null)
            return null;

        newUI.Show();
        return newUI;
    }

    public static T Hide<T>(UIType uiType) where T : UIBase
    {
        var newUI = Singleton.GetUI<T>(uiType);
        if (newUI == null)
            return null;

        newUI.Hide();
        return newUI;
    }

    public Dictionary<UIType, UIBase> ContainerPanel = new Dictionary<UIType, UIBase>();
    public Dictionary<UIType, UIBase> ContainerPopup = new Dictionary<UIType, UIBase>();

    private Transform panelRoot;
    private Transform popupRoot;

    private const string UI_Resource_PATH = "UI/";

    public void Initialize()
    {
        if (panelRoot == null)
        {
            panelRoot = new GameObject("Panel Root").transform;
            panelRoot.SetParent(transform);
        }
        if (popupRoot == null)
        {
            popupRoot = new GameObject("Popup Root").transform;
            popupRoot.SetParent(transform);
        }

        for (int index = (int)UIType.PANEL_START + 1; index < (int)UIType.PANEL_END; index++)
        {
            UIType uiType = (UIType)index;
            ContainerPanel.Add(uiType, null);
        }
        for (int index = (int)UIType.POPUP_START + 1; index < (int)UIType.POPUP_END; index++)
        {
            UIType uiType = (UIType)index;
            ContainerPopup.Add(uiType, null);
        }
    }

    public T GetUI<T>(UIType uiType) where T : UIBase
    {
        T result = null;
        if (UIType.PANEL_START < uiType && uiType < UIType.PANEL_END)
        {
            if (ContainerPanel[uiType] == null)
            {
                T loadedPrefab = Resources.Load<T>(UI_Resource_PATH + "Canvas_" + uiType.ToString());
                if (loadedPrefab != null)
                {
                    result = Instantiate(loadedPrefab, panelRoot);
                    ContainerPanel[uiType] = result;
                }
            }
            else
            {
                result = ContainerPanel[uiType] as T;
            }
        }

        if (UIType.POPUP_START < uiType && uiType < UIType.POPUP_END)
        {
            if (ContainerPopup[uiType] == null)
            {
                T loadedPrefab = Resources.Load<T>(UI_Resource_PATH + "Canvas_" + uiType.ToString());
                if (loadedPrefab != null)
                {
                    result = Instantiate(loadedPrefab, popupRoot);
                    ContainerPopup[uiType] = result;
                }
            }
            else
            {
                result = ContainerPopup[uiType] as T;
            }
        }

        return result;
    }

    public T ToggleUI<T>(UIType uiType) where T : UIBase
    {
        var ui = Singleton.GetUI<T>(uiType);
        if (ui == null) return null;

        if (ui.gameObject.activeSelf)
            ui.Hide();
        else
            ui.Show();

        return ui;
    }
}
