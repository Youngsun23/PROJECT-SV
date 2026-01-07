using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemContainer inventory;
    public void Interact(PlayerCharacterController character)
    {
        UIManager.Show<TradeUI>(UIType.Trade);
        GameManager.Singleton.TradeManager.SetTradeInventory(inventory);
    }
}
