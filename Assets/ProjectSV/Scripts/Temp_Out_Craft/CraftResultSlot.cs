using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftResultSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    // [SerializeField] private TextMeshProUGUI text;
    private Item resultItem;
    private CraftBoxPanel craftBoxPanel;

    private void Awake()
    {
        craftBoxPanel = transform.parent.GetComponent<CraftBoxPanel>();    
        Clean();
    }

    public void Set(Item item)
    {
        resultItem = item;
        icon.sprite = item.Icon;
        icon.gameObject.SetActive(true);

        // if (item.Stackable)
        // {
            // text.gameObject.SetActive(true);
            // craftBox ���� ��, ������ ���� ���� ����ֱ�
            // text.text = slot.Count.ToString();
        // }
        // else
        // {
            // text.gameObject.SetActive(false);
        // }
    }

    public void Clean()
    {
        resultItem = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        // text.gameObject.SetActive(false);
    }

    private void GetCraftedItem(Item craftedItem)
    {
        if(GameManager.Singleton.Inventory.AddItem(craftedItem))
        {
            // craftbox/�����������̳��� �����۵� count�� �ϳ��� ���(0�� �Ǹ� clear), �ٽ� ����
            CraftingSystemManager.Singleton.CraftBox.RemoveItemGrid();
            craftBoxPanel.Show();
        }
        else
        {
            Debug.Log("�κ��丮�� ���� �� �������� ȹ���� �� �����ϴ�.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(resultItem != null)
        {
            GetCraftedItem(resultItem);
        }
    }
}
