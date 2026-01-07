using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPanel : ItemPanel
{
    public override void OnClick(int id, bool isLeft)
    {
        GameManager.Singleton.TradeManager.OnClickSell(Inventory.ItemSlots[id]);

        Show();
    }
}
