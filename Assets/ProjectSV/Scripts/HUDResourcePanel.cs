using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDResourcePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTXT;

    private void Start()
    {
        UpdateHUDUIMoney(0);
    }

    public void UpdateHUDUIMoney(int value)
    {
        goldTXT.text = value.ToString();
    }
}
