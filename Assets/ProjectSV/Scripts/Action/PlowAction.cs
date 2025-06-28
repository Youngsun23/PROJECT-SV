using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowAction : ToolAction
{
    [SerializeField] private TileData plowableTile;

    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        // Debug.Log("Plow 위해 타일 클릭 중");
        
        if (tile != plowableTile)
        {
            return false;
        }

        CropManager.Singleton.Plow(pos);
        return true;
    }
}
