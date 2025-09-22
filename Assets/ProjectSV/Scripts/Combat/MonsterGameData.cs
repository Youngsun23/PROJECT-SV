using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Monster Data")]
public class MonsterGameData : ScriptableObject
{
    [field: Title("Monster Setting")]
    [field: SerializeField] public float MaxHP { get; protected set; } = 10f;
    [field: SerializeField] public float MaxStamina { get; protected set; } = 50f;

    [field: Title("Movement Setting")]
    [field: SerializeField] public float MoveSpeed { get; protected set; } = 4f;
    [field: SerializeField] public float PatrolRange { get; protected set; } = 8f;

    [field: Title("Combat Setting")]
    [field: SerializeField] public float DetectRadius { get; protected set; } = 10f;
    [field: SerializeField] public float AttackPossibleRadius { get; protected set; } = 2f;
    [field: SerializeField] public float AttackRadius { get; protected set; } = 1f;
    [field: SerializeField] public float AttackDamage { get; protected set; } = 5f;
    [field: SerializeField] public float AttackCoolTime { get; protected set; } = 2f;
    [field: SerializeField] public float SpecialAttackDamage { get; protected set; } = 10f;
    [field: SerializeField] public float DefensivePower { get; protected set; } = 0f;
}
