using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDResourcePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTXT;
    
    public void UpdateHUDUIMoney(int value)
    {
        goldTXT.text = value.ToString();
        
    }
}
