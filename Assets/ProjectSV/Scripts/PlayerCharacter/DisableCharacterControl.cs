using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCharacterControl : MonoBehaviour
{
    PlayerCharacterController characterController;
    PlayerCharacterToolController characterToolController;
    PlayerCharacterInteraction characterInteractionController;
    // 인벤, 툴바

    private void Awake()
    {
        characterController = GetComponent<PlayerCharacterController>();
        characterToolController = GetComponent<PlayerCharacterToolController>();
        characterInteractionController = GetComponent<PlayerCharacterInteraction>();
    }

    public void DisableControl()
    {
        characterController.enabled = false;
        characterToolController.enabled = false;
        characterInteractionController.enabled = false;
    }

    public void EnableControl()
    {
        characterController.enabled = true;
        characterToolController.enabled = true;
        characterInteractionController.enabled = true;
    }
}
