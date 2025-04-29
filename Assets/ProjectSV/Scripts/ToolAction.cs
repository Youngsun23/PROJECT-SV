using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 pos)
    {
        Debug.LogWarning("OnApply() Override Needed");
        return true;
    }

    public virtual bool OnApplyTileMap(Vector3Int pos, TileData tile)
    {
        Debug.LogWarning("OnApplyTileMap() Override Needed");
        return true;
    }

    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)
    {

    }
}