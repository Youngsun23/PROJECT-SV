using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Character Data")]
public class CharacterGameData : GameDataBase
{
    [field: Title("Character Setting")]
    [field: SerializeField] public float HP { get; protected set; } = 50f;
    [field: SerializeField] public float Stamina { get; protected set; } = 100f;

    [field: Title("Movement Setting")]
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 5f;
    [field: SerializeField] public float PickupRadius { get; protected set; } = 1.2f;

    [field: Title("Combat Setting")]
    [field: SerializeField] public float InvincibleTime { get; protected set; } = 1f;
    [field: SerializeField] public float AttakRadius { get; protected set; } = 0f;
    [field: SerializeField] public float AttackDamage { get; protected set; }
    [field: SerializeField] public float SpecialAttackDamage { get; protected set; }
    [field: SerializeField] public float DefensivePower { get; protected set; }

    //[field: Title("Resource Setting")]
    //[field: SerializeField] public int Coin { get; protected set; } = 0;
}
