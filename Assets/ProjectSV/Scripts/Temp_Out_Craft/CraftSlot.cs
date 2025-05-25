using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private int index;

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.Item.Icon;
        icon.gameObject.SetActive(true);
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // itemPanel.OnClick(index);
    }

    public void Highlight(bool tf)
    {
        // highlighted.gameObject.SetActive(tf);
    }
}
