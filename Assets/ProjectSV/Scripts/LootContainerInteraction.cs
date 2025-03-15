using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteraction : Interactable
{
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private bool opened = false;
    public Animator animator;

    public override void Interact(PlayerCharacterController character)
    {
        if(!opened)
        {
            opened = true;
            animator.SetTrigger("Interact");
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
        }
    }
}
