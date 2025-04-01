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
    private Dictionary<Vector3Int, Crop> plowedTiles;

    private void Start()
    {
        plowedTiles = new Dictionary<Vector3Int, Crop>();
    }

    public bool plowedCheck(Vector3Int pos)
    {
        return plowedTiles.ContainsKey(pos);
    }

    public void Plow(Vector3Int pos)
    {
        if (plowedTiles.ContainsKey(pos))
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
        plowedTiles.Add(pos, crop);

        plowTargetTileMap.SetTile(pos, plowed);
    }
}
