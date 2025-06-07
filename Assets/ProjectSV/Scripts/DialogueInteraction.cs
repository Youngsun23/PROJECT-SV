using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : Interactable
{
    [SerializeField] private DialogueData dialogue;
    public override void Interact(PlayerCharacterController character)
    {
        DialogueManager.Singleton.StartDialogue(dialogue);
    }
}
