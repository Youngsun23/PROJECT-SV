using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plant")]
public class PlantAction : ToolAction
{
    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, TileMapReadManager tileMapReadManager)
    {
        if(tileMapReadManager.CropManager.IsPlantable(pos))
        {
            tileMapReadManager.CropManager.Plant(pos);
            return true;
        }

        return false;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory) // ItemContainer�� PC�κ��丮 �ܿ��� ����Ŵϱ�
    {
        inventory.RemoveItem(usedItem);
    }
}
