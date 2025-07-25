using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place")]
public class PlaceAction : ToolAction
{
    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        PlaceableObjectsManager.Singleton.Place(usedItem, pos);

        return true;
    }
}
