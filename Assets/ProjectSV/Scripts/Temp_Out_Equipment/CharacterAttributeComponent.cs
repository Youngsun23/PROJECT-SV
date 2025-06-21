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

    //public void RegisterEvent(AttributeTypes type, System.Action<float, float> onChanedEvent = null, System.Action<float> onChangedBuffed = null)
    //{
    //    attributes[type].OnChangedEvent += onChanedEvent;
    //    attributes[type].OnChangedBuffed += onChangedBuffed;
    //}

    //public void EraseEvent(AttributeTypes type, System.Action<float, float> onChanedEvent = null, System.Action<float> onChangedBuffed = null)
    //{
    //    attributes[type].OnChangedEvent -= onChanedEvent;
    //    attributes[type].OnChangedBuffed -= onChangedBuffed;
    //}

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

    public void SetAttribute(AttributeTypes type, float defaultValue, float modifierValue = 0/*, float decreaseValue = 0*/)
    {
        attributes[type].DefaultValue = defaultValue;
        attributes[type].ModifierValue = modifierValue;
        // attributes[type].DecreaseValue = decreaseValue;
    }
}
