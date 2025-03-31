using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class TileMapReadManager : MonoBehaviour
{
    [SerializeField] private Tilemap readTargetTileMap;
    [SerializeField] private List<TileData> tileData;
    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData data in tileData)
        {
            foreach (TileBase tile in data.Tiles)
            {
                dataFromTiles.TryAdd(tile, data);
            }
        }
    }

    public Vector3Int GetGridPosition(Vector2 pos, bool mousePos = true)
    {
        Vector3 worldPos;

        if (mousePos)
        {
            worldPos = Camera.main.ScreenToWorldPoint(pos);
        }
        else
        {
            worldPos = pos;
        }

        Vector3Int gridPos = readTargetTileMap.WorldToCell(worldPos);
        return gridPos;
    }

    public TileBase GetTileBase(Vector3Int gridPos)
    {
        TileBase tile = readTargetTileMap.GetTile(gridPos);

        // Debug.Log($"Tile in Position {gridPos} is {tile}");

        return tile;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        if (tileBase != null && dataFromTiles.ContainsKey(tileBase))
        {
            return dataFromTiles[tileBase];
        }

        return null;
    }
}
