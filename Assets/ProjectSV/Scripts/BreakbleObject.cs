using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakbleObject : MonoBehaviour, IDamage
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;

    private void Start()
    {
        maxHP = Random.Range(1, 3);
        currentHP = maxHP;
    }

    public void TakeDamage(IActor actor, float damage)
    {
        // �ִϸ�����
        // ����Ʈ
        // ī�޶� ����ũ

        currentHP--;

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
