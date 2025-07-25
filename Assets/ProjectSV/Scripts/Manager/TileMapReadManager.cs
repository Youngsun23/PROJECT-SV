using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadManager : SingletonBase<TileMapReadManager>
{
    public Tilemap TargetMap => readTargetTileMap;
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

    public void SetReadTargetTileMap(Tilemap target)
    {
        readTargetTileMap = target; 
    }

    public Vector3Int GetGridPosition(Vector2 mousePos, bool useMouse = true)
    {
        if(readTargetTileMap == null)
        {
            return Vector3Int.zero;

            // Find 쓰는 대신, 새로운 씬 입장 시 외부에서 Set 해줄거임 (CropManager도 동일)
            // readTargetTileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        Vector3 worldPos;

        if (useMouse)
        {
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        else // 얘가 필요한 때가?
        {
            worldPos = mousePos;
        }

        Vector3Int gridPos = readTargetTileMap.WorldToCell(worldPos);
        // Debug.Log($"WorldToCell: {worldPos} -> {gridPos}");
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
        if (readTargetTileMap == null)
        {
            return null;

            // readTargetTileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if (tileBase != null)
        {
            // Debug.Log($"TileData: {dataFromTiles[tileBase].name}");
            dataFromTiles.TryGetValue(tileBase, out TileData tileData);
            return tileData;
        }
        return null;
    }
}
