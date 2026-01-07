using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPanel : ItemPanel
{
    protected override ItemContainer Inventory => tradeInventory;
    private ItemContainer tradeInventory;
    [SerializeField] private GameObject buyButtonPrefab;

    protected override void Start()
    {
        SetInventory(GameManager.Singleton.TradeManager.TradeInventory);
        SetPanelUI();

        base.Start();
    }

    private void SetInventory(ItemContainer inventory)
    {
        tradeInventory = inventory;
    }

    private void SetPanelUI()
    {
        int slotCount = Inventory.ItemSlots.Count;

        for(int i = 0; i < slotCount; i++)
        {
            GameObject buyButton = Instantiate(buyButtonPrefab);
            buttons.Add(buyButton.GetComponent<BuyButton>());
        }
    }

    public override void OnClick(int id, bool isLeft)
    {
        GameManager.Singleton.TradeManager.OnClickBuy(Inventory.ItemSlots[id]);

        Show();
    }
}
