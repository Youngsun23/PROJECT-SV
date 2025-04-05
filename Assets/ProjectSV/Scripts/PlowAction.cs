using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowAction : ToolAction
{
    [SerializeField] private TileData plowableTile;

    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, TileMapReadManager tileMapReadManager)
    {
        if (tile != plowableTile)
            return false;

        tileMapReadManager.CropManager.Plow(pos);
        return true;
    }
}
