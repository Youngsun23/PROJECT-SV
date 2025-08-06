using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryInteraction : MonoBehaviour, IInteractable
{
    // [SerializeField] private ItemContainer container;
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private bool opened = false;
    public Animator animator;

    public void Interact(PlayerCharacterController character)
    {
        if (!opened)
        {
            opened = true;
            animator.SetTrigger("Interact");
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);

            // 애니메이션 출력 후 UI 띄우기 - 코루틴/애니메이터에서 UIShow 호출/?

            UIManager.Show<ChestInventoryUI>(UIType.ChestInventory);
        }
        else
        {
            opened = false;
            animator.SetTrigger("Interact");
            chestOpen.SetActive(false);
            chestClosed.SetActive(true);

            UIManager.Hide<ChestInventoryUI>(UIType.ChestInventory);
        }
    }
}