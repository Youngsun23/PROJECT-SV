using HAD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop
{
    
}

public class CropManager : SingletonBase<CropManager>
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
        // 1. plowed���� �ʾ����� -> farmingTiles�� ��ϵǾ��������� -> return
        // 2. �̹� crop�� ��ϵǾ� ������ -> �̹� �ٸ� �۹� �ִ� �� -> farmingTiles[pos]�� null�� �ƴ� -> return
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
