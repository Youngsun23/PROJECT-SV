using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int index;
    [SerializeField] private Image highlighted;
    private ItemPanel itemPanel;
    private ToolTipPanel toolTipPanel;
    private bool belongsInventory = false;

    private void Awake()
    {
        itemPanel = transform.parent.GetComponent<ItemPanel>();
    }

    private void Start()
    {
        // var toolTipUI = UIManager.Singleton.GetUI<ToolTipUI>(UIType.ToolTip);
        InventoryPanel inventoryPanel = itemPanel as InventoryPanel;

        if (inventoryPanel != null)
        {
            toolTipPanel = inventoryPanel.ToolTipPanel;
            belongsInventory = true;
        }
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.Item.Icon;
        icon.gameObject.SetActive(true);    

        if(slot.Item.Stackable)
        {
            text.gameObject.SetActive(true);
            text.text = slot.Count.ToString();  
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                itemPanel.OnClick(index, true);
                break;
            case PointerEventData.InputButton.Right:
                itemPanel.OnClick(index, false);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!belongsInventory) return;

        Item item = GameManager.Singleton.Inventory.ItemSlots[index].Item;

        if (item != null)
        {
            UIManager.Show<ToolTipUI>(UIType.ToolTip);
            toolTipPanel.UpdateToolTip(item.Name, item.Info);
            //ToolTipPanel.Singleton.Show(item.Name, item.Info);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!belongsInventory) return;
        //ToolTipPanel.Singleton.Hide();
        UIManager.Hide<ToolTipUI>(UIType.ToolTip);
    }

    public void Highlight(bool tf)
    {
        highlighted.gameObject.SetActive(tf);
    }
}
