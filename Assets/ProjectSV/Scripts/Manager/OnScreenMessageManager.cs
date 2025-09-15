using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnScreenMessageManager : SingletonBase<OnScreenMessageManager>
{
    [SerializeField] private GameObject textPrefab;
    private float timeOnScreen = 5f;

    public void ShowMessageOnScreen(Vector3 worldPos, string message)
    {
        worldPos.x += Random.Range(-0.5f, 0.5f);
        worldPos.y += Random.Range(-0.5f, 0.5f);

        GameObject textGO = Instantiate(textPrefab, transform);
        textGO.transform.position = worldPos;

        TextMeshPro tmp = textGO.GetComponent<TextMeshPro>();
        tmp.text = message;

        Destroy(textGO, timeOnScreen);

        // �÷��� �ִϸ��̼�...
    }

    // ȹ�� ������ ���� �޽��� ó�� �Լ��� ����?
}
