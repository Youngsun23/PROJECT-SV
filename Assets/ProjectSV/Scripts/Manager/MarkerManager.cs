using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private Tilemap targetTileMap;
    [SerializeField] private TileBase tile;
    private Vector3Int markedCellPosition;
    private Vector3Int previousMarkedCellPosition;
    private bool isMarkerVisible;

    private void Update()
    {
        if (!isMarkerVisible)
            return;
        
        targetTileMap.SetTile(previousMarkedCellPosition, null);
        targetTileMap.SetTile(markedCellPosition, tile);
        previousMarkedCellPosition = markedCellPosition;
    }

    public void SetMarkedCellPosition(Vector3Int pos)
    {
        markedCellPosition = pos;
    }

    public void ToggleMarker(bool selectable)
    {
        isMarkerVisible = selectable;
        targetTileMap.gameObject.SetActive(isMarkerVisible);
    }
}
