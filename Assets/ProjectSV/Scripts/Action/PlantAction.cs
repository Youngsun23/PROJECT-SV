using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plant")]
public class PlantAction : ToolAction
{
    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        Debug.Log("여기를 못 들어와");
        if(CropManager.Singleton.IsPlantable(pos))
        {
            CropManager.Singleton.Plant(pos, usedItem.CropData);
            return true;
        }

        return false;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
    }
}
