using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerCharacter : SingletonBase<PlayerCharacter>, IActor, IDamage
{
    public CharacterAttributeComponent CharacterAttributeComponent => characterAttributeComponent;
    private CharacterAttributeComponent characterAttributeComponent;
    public CharacterGameData characterData;
    private HUDBarPanel HUDUI;

    protected override void Awake()
    {
        characterAttributeComponent = GetComponent<CharacterAttributeComponent>();
    }

    private void Start()
    {
        HUDUI = UIManager.Singleton.GetUI<HUDUI>(UIType.HUD).gameObject.GetComponent<HUDBarPanel>();
        characterAttributeComponent.RegisterEvent(AttributeTypes.HP, (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur), (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur));
        characterAttributeComponent.RegisterEvent(AttributeTypes.Stamina, (int max, int cur) => HUDUI.UpdateHUDUIStamina(max, cur), (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur));
    }

    private void OnDestroy()
    {
        characterAttributeComponent.EraseEvent(AttributeTypes.HP, (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur), (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur));
        characterAttributeComponent.EraseEvent(AttributeTypes.Stamina, (int max, int cur) => HUDUI.UpdateHUDUIStamina(max, cur), (int max, int cur) => HUDUI.UpdateHUDUIHP(max, cur));
    }

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

    public void Attack(Item usedWeapon)
    {
        // usedWeapon의 정보 가져와서 그만큼의 사거리, 속도, 대미지
        WeaponItem weapon = usedWeapon as WeaponItem;
        Collider2D[] overlapObjects = Physics2D.OverlapCircleAll(transform.position, weapon.Radius);
        if (overlapObjects?.Length == 0)
        {
            return;
        }
        for (int i = 0; i < overlapObjects.Length; i++)
        {
            Vector2 position = overlapObjects[i].transform.position;
            Vector2 direction = (position - (Vector2)transform.position).normalized;
            float dotAngle = Vector2.Dot(transform.up, direction);
            if (dotAngle > 0.5f)
            {
                if (overlapObjects[i].CompareTag("Monster"))
                {
                    var damageInterface = overlapObjects[i].GetComponent<IDamage>();
                    damageInterface.TakeDamage(this, weapon.Damage);
                    Debug.Log($"{overlapObjects[i].name}에게 {weapon.Damage}만큼의 피해");
                }
            }
        }
    }

    public GameObject GetActor()
    {
        return this.gameObject;
    }

    public void TakeDamage(IActor actor, int damage)
    {
        // 애니메이터
        // 이펙트
        // 카메라 쉐이크

        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.HP, -damage);
        float currentHP = characterAttributeComponent.GetAttribute(AttributeTypes.HP).CurrentValue;
        
        // HUD UI 갱신

        if(currentHP <= 0f)
        {
            PassOut();
        }
    }

    public void Heal(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.HP, val);
        CharacterAttribute hpAttribute = characterAttributeComponent.GetAttribute(AttributeTypes.HP);

        if(hpAttribute.CurrentValue > hpAttribute.MaxValue)
        {
            characterAttributeComponent.SetBuffedAttribute(AttributeTypes.HP, 0);
        }
    }

    public void FullHeal()
    {
        characterAttributeComponent.SetBuffedAttribute(AttributeTypes.HP, 0);
    }

    public void PassOut()
    {

    }

    public void UseStamina(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.Stamina, -val);
        float currentStamina = characterAttributeComponent.GetAttribute(AttributeTypes.Stamina).CurrentValue;

        // HUD UI 갱신

        if (currentStamina <= 0f)
        {
            GetExhausted();
        }
    }

    public void RecoverStamina(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.Stamina, val);
        CharacterAttribute staminaAttribute = characterAttributeComponent.GetAttribute(AttributeTypes.Stamina);

        if (staminaAttribute.CurrentValue > staminaAttribute.MaxValue)
        {
            characterAttributeComponent.SetBuffedAttribute(AttributeTypes.Stamina, 0);
        }
    }

    public void FullRecoverStamina()
    {
        characterAttributeComponent.SetBuffedAttribute(AttributeTypes.Stamina, 0);
    }

    public void GetExhausted()
    {

    }
}