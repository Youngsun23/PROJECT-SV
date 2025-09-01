using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttributeTypes
{
    HP,
    Stamina,
    MoveSpeed, 
    PickupRadius,
    //AttackRadius,
    //AttackDamage,
    //SpecialAttackDamage,
    //DefensivePower,

    EndField,
}

public class CharacterAttributeComponent : MonoBehaviour
{
    public Dictionary<AttributeTypes, CharacterAttribute> attributes = new Dictionary<AttributeTypes, CharacterAttribute>();

    public void RegisterEvent(AttributeTypes type, System.Action<int, int> onModifierChanged = null, System.Action<int, int> onBuffedChanged = null)
    {
        attributes[type].OnModifierChanged += onModifierChanged;
        attributes[type].OnBuffedChanged += onBuffedChanged;
    }

    public void EraseEvent(AttributeTypes type, System.Action<int, int> onModifierChanged = null, System.Action<int, int> onBuffedChanged = null)
    {
        attributes[type].OnModifierChanged -= onModifierChanged;
        attributes[type].OnBuffedChanged -= onBuffedChanged;
    }

    private void Awake()
    {
        for (int i = 0; i < (int)AttributeTypes.EndField; i++)
        {
            attributes.Add((AttributeTypes)i, new CharacterAttribute());
        }
    }

    public CharacterAttribute GetAttribute(AttributeTypes type)
    {
        return attributes[type];
    }

    public float GetAttributeCurrentValue(AttributeTypes type)
    {
        return attributes[type].CurrentValue;
    }

    public void SetAttribute(AttributeTypes type, float defaultValue, float modifierValue = 0, float buffedValue = 0)
    {
        attributes[type].DefaultValue = defaultValue;
        attributes[type].ModifierValue = modifierValue;
        attributes[type].BuffedValue = buffedValue;
    }

    public void ChangeModifierAttribute(AttributeTypes type, float modifierValue)
    {
        attributes[type].ModifierValue += modifierValue;
    }

    public void SetModifierAttribute(AttributeTypes type, float modifierValue)
    {
        attributes[type].ModifierValue = modifierValue;
    }

    public void ChangeBuffedAttribute(AttributeTypes type, float buffedValue)
    {
        attributes[type].BuffedValue += buffedValue;
    }

    public void SetBuffedAttribute(AttributeTypes type, float buffedValue)
    {
        attributes[type].BuffedValue = buffedValue;
    }
}
