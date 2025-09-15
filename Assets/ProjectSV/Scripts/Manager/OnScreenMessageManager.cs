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

        // 플로팅 애니메이션...
    }

    // 획득 아이템 등의 메시지 처리 함수는 따로?
}
