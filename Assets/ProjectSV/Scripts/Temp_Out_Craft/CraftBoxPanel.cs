using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftBoxPanel : MonoBehaviour
{
    private ItemContainer CraftBox => CraftingSystemManager.Singleton.CraftBox;
    [SerializeField] private List<CraftSlot> slots;
    [SerializeField] private CraftResultSlot resultSlot;

    protected virtual void Start()
    {
        CraftBox.onChange += Show;
        Initialize();
    }

    private void OnDestroy()
    {
        CraftBox.onChange -= Show;
    }

    private void OnEnable()
    {
        Show();
    }

    public void Initialize()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < CraftBox.ItemSlots.Count && i < slots.Count; i++)
        {
            slots[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < CraftBox.ItemSlots.Count && i < slots.Count; i++)
        {
            if (CraftBox.ItemSlots[i].Item == null)
            {
                slots[i].Clean();
            }
            else
            {
                slots[i].Set(CraftBox.ItemSlots[i]);
            }
        }

        // craftbox�� ���°� �ٲ� �� ȣ��Ǵ� �̺�Ʈ�� �и��� �� ������ ���ڴµ� ��...
        Item craftableItem = CraftingSystem.Singleton.TryCraft(ToItemTypeArray());
        if (craftableItem != null)
        {
            resultSlot.Set(craftableItem);
        }
        else
        {
            resultSlot.Clean();
        }
    }

    public virtual void OnClick(int id, bool isLeft)
    {
        if (isLeft)
            GameManager.Singleton.ItemDragDropController.OnLeftClick(CraftBox.ItemSlots[id]);
        else
            GameManager.Singleton.ItemDragDropController.OnRightClick(CraftBox.ItemSlots[id]);

        Show();
    }

    public ItemType[] ToItemTypeArray()
    {
        ItemType[] result = new ItemType[CraftBox.ItemSlots.Count];

        for (int i = 0; i < CraftBox.ItemSlots.Count; i++)
        {
            // �� ��ũ��Ʈ ��ü �� ���ǵ� ���� ���� �̰͸�
            // result[i] = CraftBox.ItemSlots[i]?.Item?.Type ?? ItemType.None;
        }

        return result;
    }
}
