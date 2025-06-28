using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogue;
    public void Interact(PlayerCharacterController character)
    {
        GameManager.Singleton.DialogueManager.StartDialogue(dialogue);
    }
}
