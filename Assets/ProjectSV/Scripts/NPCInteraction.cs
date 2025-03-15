using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInteraction : Interactable
{
    [SerializeField] private GameObject textCanvas;
    [SerializeField] private TextMeshProUGUI textUI;

    public override void Interact(PlayerCharacterController character)
    {
        Debug.Log("세월아~ 네월아~");

        textCanvas.SetActive(true);
        textUI.text = "세월아~ 네월아~";
    }
}
