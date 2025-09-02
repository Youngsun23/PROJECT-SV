using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatBase : MonoBehaviour, IActor, IDamage
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public GameObject GetActor()
    {
        return this.gameObject;
    }

    public void TakeDamage(IActor actor, int damage)
    {
        // �ִϸ�����
        // ����Ʈ
        // ī�޶� ����ũ

        currentHP -= damage;

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {

    }
    
}