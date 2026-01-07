using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyButton : ItemPanelButton
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPrice;

    public override void Set(ItemSlot slot)
    {
        icon.sprite = slot.Item.Icon;
        icon.gameObject.SetActive(true);

        textName.text = slot.Item.Name;
        textName.gameObject.SetActive(true);

        textPrice.text = slot.Item.BuyPrice.ToString();
        textPrice.gameObject.SetActive(true);
    }

    public override void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        textName.gameObject.SetActive(false);
        textPrice.gameObject.SetActive(false);
    }

    public void Sold()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        itemPanel.OnClick(index, true);
    }
}
