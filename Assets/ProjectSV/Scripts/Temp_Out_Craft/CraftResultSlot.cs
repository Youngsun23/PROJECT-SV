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
            // craftBox 상태 상, 가능한 제작 개수 띄워주기
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
            // craftbox/아이템컨테이너의 아이템들 count들 하나씩 깎고(0개 되면 clear), 다시 판정
            CraftingSystemManager.Singleton.CraftBox.RemoveItemGrid();
            craftBoxPanel.Show();
        }
        else
        {
            Debug.Log("인벤토리가 가득 차 아이템을 획득할 수 없습니다.");
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
