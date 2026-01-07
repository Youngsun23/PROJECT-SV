using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public ItemContainer TradeInventory { get; private set; }

    public void SetTradeInventory(ItemContainer inventory)
    {
        TradeInventory = inventory;
    }

    public void OnClickSell(ItemSlot itemSlot)
    {
        int count = itemSlot.Count;
        int price = itemSlot.Item.SellPrice;
        int totalPrice = count * price;
        PlayerCharacter.Singleton.CharacterResourceComponent.ChangeResource(ResourceTypes.Coin, totalPrice);
        itemSlot.Clear();
    }

    public void OnClickBuy(ItemSlot item)
    {
        int playerCoin = PlayerCharacter.Singleton.CharacterResourceComponent.GetResourceValue(ResourceTypes.Coin);

        if (playerCoin < item.Item.BuyPrice)
            return;

        if (GameManager.Singleton.Inventory.IsFull())
            return;

        GameManager.Singleton.Inventory.AddItem(item.Item);
        PlayerCharacter.Singleton.CharacterResourceComponent.ChangeResource(ResourceTypes.Coin, -item.Item.BuyPrice);
    }
}
