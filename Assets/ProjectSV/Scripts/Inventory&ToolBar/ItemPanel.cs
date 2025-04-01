using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    protected ItemContainer Inventory => GameManager.Instance.Inventory;
    [SerializeField] protected List<InventoryButton> buttons;

    protected virtual void Start()
    {
        Inventory.onChange += Show;
        Initialize();
    }

    public void Initialize()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < Inventory.ItemSlots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < Inventory.ItemSlots.Count && i < buttons.Count; i++) // ����, �κ��� ��ư ���� �ٸ��ϱ� ��ư �ִ� ������ŭ�� �κ��̶� �����ؾ���
        {
            if (Inventory.ItemSlots[i].Item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(Inventory.ItemSlots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {
        
    }
}
