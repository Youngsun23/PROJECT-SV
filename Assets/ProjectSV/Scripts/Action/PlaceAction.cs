using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place")]
public class PlaceAction : ToolAction
{
    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        Debug.Log("액션 호출");

        return PlaceableObjectsManager.Singleton.Place(usedItem, pos);
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
        GameManager.Singleton.PlaceableItemHighlight.Show(false);
    }
}
