using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelButton : MonoBehaviour, IPointerClickHandler
{
    protected ItemPanel itemPanel;
    [SerializeField] protected int index;
    [SerializeField] protected Image highlighted;

    protected virtual void Awake()
    {
        itemPanel = transform.parent.GetComponent<ItemPanel>();
    }

    public virtual void SetIndex(int _index)
    {
        index = _index;
    }

    public virtual void Set(ItemSlot slot)
    {

    }

    public virtual void Clean()
    {

    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public virtual void Highlight(bool tf)
    {
        highlighted.gameObject.SetActive(tf);
    }
}
