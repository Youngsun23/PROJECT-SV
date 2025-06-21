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

    // Equip -> 호출
    public void ApplyEffect()
    {
        // 유저데이터 갱신
        // 캐릭터이니셜라이즈() 호출
        //UserDataManager.Singleton.UpdateUserDataEquipment(this, true);
        //PlayerCharacter.Singleton.InitializeCharacterAttribute();
    }

    // UnEquip -> 호출
    public void RemoveEffect()
    {
        //UserDataManager.Singleton.UpdateUserDataEquipment(this, false);
        //PlayerCharacter.Singleton.InitializeCharacterAttribute();
    }
}
