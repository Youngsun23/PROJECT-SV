using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

// -> ĳ����, ����, ... [C] (���� ��ġ�� ��� ����)
// ���� ���ʿ� Z �Ʒ���

public class ZDepth : MonoBehaviour
{
    private Transform tf;
    [SerializeField] private bool stationary = true;

    private void Start()
    {
        tf = transform;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = pos.y * 0.001f;
        transform.position = pos;

        // spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);

        if (stationary)
        {
            Destroy(this);
        }
    }
}
