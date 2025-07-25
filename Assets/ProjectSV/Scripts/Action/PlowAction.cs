using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowAction : ToolAction
{
    [SerializeField] private TileData plowableTile;
    // [SerializeField] private AudioClip plowSound;

    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        if (tile != plowableTile)
        {
            return false;
        }

        CropManager.Singleton.Plow(pos);
        return true;
    }
}
