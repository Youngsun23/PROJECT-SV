using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogue;

    public void Interact(PlayerCharacterController character)
    {
        UIManager.Show<DialogueUI>(UIType.Dialogue);

        GameManager.Singleton.DialogueManager.StartDialogue(dialogue);
    }
}
