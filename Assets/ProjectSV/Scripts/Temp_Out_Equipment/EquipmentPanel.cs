using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    private ItemContainer EquipmentBox => EquipmentSystemManager.Singleton.EquipmentBox;
    [SerializeField] private List<EquipmentSlot> slots;
    [SerializeField] private TextMeshProUGUI HPModifierText;
    [SerializeField] private TextMeshProUGUI staminaModifierText;
    [SerializeField] private TextMeshProUGUI moveSpeedModifierText;
    [SerializeField] private TextMeshProUGUI pickupRadiusModifierText;

    protected virtual void Start()
    {
        EquipmentBox.onChange += Show;
        Initialize();
    }

    private void OnDestroy()
    {
        EquipmentBox.onChange -= Show;
    }

    private void OnEnable()
    {
        Show();
    }

    private void OnDisable()
    {
        UserDataManager.Singleton.UpdateUserDataEquipmentAtOnce(EquipmentBox);
        PlayerCharacter.Singleton.InitializeCharacterAttribute();
    }

    public void Initialize()
    {
        SetIndex();
        Show();

        // Load�� �Ŀ�, ����� ���������� ��� ����� UI�� ���
        // LoadInitialize();
    }

    //private void LoadInitialize()
    //{
    //    // List<EquipItem> GetUserDataEquippedItems() ���������� �����ͼ�
    //    // List ���鼭 EquipmentBox.ItemSlots[i].Set()���ֱ�
    //}

    private void SetIndex()
    {
        for (int i = 0; i < EquipmentBox.ItemSlots.Count && i < slots.Count; i++)
        {
            slots[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < EquipmentBox.ItemSlots.Count && i < slots.Count; i++)
        {
            if (EquipmentBox.ItemSlots[i].Item == null)
            {
                slots[i].Clean();
            }
            else
            {
                slots[i].Set(EquipmentBox.ItemSlots[i]);
            }
        }

        ClearText();
    }

    //private void UpdateLoaded()
    //{
    //    foreach(EquipItem item in UserDataManager.Singleton.GetUserDataEquippedItems())
    //    {
    //        for(int i = 0; i < slots.Count; i++)
    //        {
    //            if (slots[i].Type == item.EquipmentType)
    //            {
    //                slots[i].Set(EquipmentBox.ItemSlots[i]);
    //            }
    //        }
    //    }
    //}

    public virtual void OnClick(int id)
    {
        // �巡�׵�����Կ� �� �� ���� �� üũ x
        if (GameManager.Singleton.ItemDragDropController.DragDropSlot.Item != null)
        {
            EquipItem equip = GameManager.Singleton.ItemDragDropController.DragDropSlot.Item as EquipItem;
            if (equip == null)
                return;
            if (slots[id].Type != equip.EquipmentType)
                return;
        }
        
        GameManager.Singleton.ItemDragDropController.OnLeftClick(EquipmentBox.ItemSlots[id]);

        Show();

        EquipItem hilightedItem = EquipmentBox.ItemSlots[id].Item as EquipItem;
        if (hilightedItem != null)
        {
            if(hilightedItem.Effects.ContainsKey(AttributeTypes.HP))
            {
                HPModifierText.text = hilightedItem.Effects[AttributeTypes.HP].ToString();
            }
            if (hilightedItem.Effects.ContainsKey(AttributeTypes.Stamina))
            {
                staminaModifierText.text = hilightedItem.Effects[AttributeTypes.Stamina].ToString();
            }
            if (hilightedItem.Effects.ContainsKey(AttributeTypes.MoveSpeed))
            {
                moveSpeedModifierText.text = hilightedItem.Effects[AttributeTypes.MoveSpeed].ToString();
            }
            if (hilightedItem.Effects.ContainsKey(AttributeTypes.PickupRadius))
            {
                pickupRadiusModifierText.text = hilightedItem.Effects[AttributeTypes.PickupRadius].ToString();
            }
        }
    }

    private void ClearText()
    {
        HPModifierText.text = "";
        staminaModifierText.text = "";
        moveSpeedModifierText.text = "";
        pickupRadiusModifierText.text = "";
    }
}