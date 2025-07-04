using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int index;
    private CraftBoxPanel craftBoxPanel;

    private void Awake()
    {
        craftBoxPanel = transform.parent.GetComponent<CraftBoxPanel>(); 
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.Item.Icon;
        icon.gameObject.SetActive(true);

        if (slot.Item.Stackable)
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
                craftBoxPanel.OnClick(index, true);
                break;
            case PointerEventData.InputButton.Right:
                craftBoxPanel.OnClick(index, false);
                break;
        }
    }

    public void Highlight(bool tf)
    {
        // highlighted.gameObject.SetActive(tf);
    }
}
