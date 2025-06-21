using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    None,

    Head,
    Body,
    Tool,
    Shard,
    Shoes,

    Else
}

public interface IEquipable
{
    void ApplyEffect();
    void RemoveEffect();
}

[CreateAssetMenu (menuName = "Data/EquipItem")]
public class EquipItem : Item, IEquipable
{
    public SerializableDictionary<AttributeTypes, float> Effects => effects;
    public EquipmentType EquipmentType => equipmentType;

    [SerializeField] private SerializableDictionary<AttributeTypes, float> effects;
    [SerializeField] private EquipmentType equipmentType;

    // Equip -> ȣ��
    public void ApplyEffect()
    {
        // ���������� ����
        // ĳ�����̴ϼȶ�����() ȣ��
        //UserDataManager.Singleton.UpdateUserDataEquipment(this, true);
        //PlayerCharacter.Singleton.InitializeCharacterAttribute();
    }

    // UnEquip -> ȣ��
    public void RemoveEffect()
    {
        //UserDataManager.Singleton.UpdateUserDataEquipment(this, false);
        //PlayerCharacter.Singleton.InitializeCharacterAttribute();
    }
}
