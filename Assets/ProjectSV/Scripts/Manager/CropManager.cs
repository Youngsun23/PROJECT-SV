using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop
{
    
}

public class CropManager : MonoBehaviour
{
    [SerializeField] private TileBase plowed;
    [SerializeField] private TileBase planted;
    [SerializeField] private Tilemap plowTargetTileMap;
    [SerializeField] private Tilemap plantTargetTileMap;
    private Dictionary<Vector3Int, Crop> farmingTiles;

    private void Start()
    {
        farmingTiles = new Dictionary<Vector3Int, Crop>();
    }

    public bool IsPlantable(Vector3Int pos)
    {
        // 1. plowed되지 않았으면 -> farmingTiles에 등록되어있지않음 -> return
        // 2. 이미 crop이 등록되어 있으면 -> 이미 다른 작물 있는 거 -> farmingTiles[pos]가 null이 아님 -> return
        if (!farmingTiles.ContainsKey(pos) || farmingTiles[pos] != null)
            return false;
        else
            return true;
    }

    public void Plow(Vector3Int pos)
    {
        if (farmingTiles.ContainsKey(pos))
            return;

        CreatePlowedTile(pos);
    }

    public void Plant(Vector3Int pos)
    {
        plantTargetTileMap.SetTile(pos, planted);
        Crop crop = new Crop();
        farmingTiles[pos] = crop;
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        farmingTiles.Add(pos, null);

        plowTargetTileMap.SetTile(pos, plowed);
    }
}
