using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : SingletonBase<PlayerCharacter>
{
    public CharacterAttributeComponent CharacterAttributeComponent => characterAttributeComponent;
    private CharacterAttributeComponent characterAttributeComponent;
    public CharacterGameData characterData;

    protected override void Awake()
    {
        characterAttributeComponent = GetComponent<CharacterAttributeComponent>();
    }

    //private void Start()
    //{
    //    characterAttributeComponent.RegisterEvent(AttributeTypes.HealthPoint, (float max, float cur) => HUDUI.Instance.UpdateHUDUIHP(max, cur));
    //    characterAttributeComponent.RegisterEvent(AttributeTypes.MagicArrowCount, (float max, float cur) => HUDUI.Instance.UpdateHUDUIMagic(max, cur));
    //}

    //private void OnDestroy()
    //{
    //    characterAttributeComponent.EraseEvent(AttributeTypes.HealthPoint, (float max, float cur) => HUDUI.Instance.UpdateHUDUIHP(max, cur));
    //    characterAttributeComponent.EraseEvent(AttributeTypes.MagicArrowCount, (float max, float cur) => HUDUI.Instance.UpdateHUDUIMagic(max, cur));
    //}

    public void InitializeCharacterAttribute()
    {
        Dictionary<AttributeTypes, float> totalEquipmentModifier = new();

        // 입은 장비 체크
        // 향후 -> EquipItem 저장하는 대신 ID(int) List로 저장해서 GameDataManager에 등록한 아이템 List에 접근, 정보 빼오기?
        foreach (EquipItem item in UserDataManager.Singleton.GetUserDataEquippedItems())
        {
            if (item == null || item.Effects == null) continue;

            foreach (var effect in item.Effects)
            {
                if (totalEquipmentModifier.ContainsKey(effect.Key))
                    totalEquipmentModifier[effect.Key] += effect.Value;
                else
                    totalEquipmentModifier[effect.Key] = effect.Value;
            }
        }

        characterAttributeComponent.SetAttribute(AttributeTypes.HP, characterData.HP, totalEquipmentModifier.TryGetValue(AttributeTypes.HP, out float mHP) ? mHP : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.Stamina, characterData.Stamina, totalEquipmentModifier.TryGetValue(AttributeTypes.Stamina, out float mStamina) ? mStamina : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.MoveSpeed, characterData.MoveSpeed, totalEquipmentModifier.TryGetValue(AttributeTypes.MoveSpeed, out float mMoveSpeed) ? mMoveSpeed : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.PickupRadius, characterData.PickupRadius, totalEquipmentModifier.TryGetValue(AttributeTypes.PickupRadius, out float mPickupRadius) ? mPickupRadius : 0f);

        // HUDUI.Instance.UpdateHUDUIHP(MaxHP, CurHP);
    }
}
