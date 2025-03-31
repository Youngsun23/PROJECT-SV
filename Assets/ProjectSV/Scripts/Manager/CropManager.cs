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
    private Dictionary<Vector2Int, Crop> plowedTiles;

    private void Start()
    {
        plowedTiles = new Dictionary<Vector2Int, Crop>();
    }

    public bool Check(Vector3Int pos)
    {
        return plowedTiles.ContainsKey((Vector2Int)pos);
    }

    public void Plow(Vector3Int pos)
    {
        if (plowedTiles.ContainsKey((Vector2Int)pos))
            return;

        CreatePlowedTile(pos);
    }

    public void Plant(Vector3Int pos)
    {
        plantTargetTileMap.SetTile(pos, planted);
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        Crop crop = new Crop();
        plowedTiles.Add((Vector2Int)pos, crop);

        plowTargetTileMap.SetTile(pos, plowed);
    }
}
