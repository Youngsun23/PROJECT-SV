using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : SingletonBase<PlaceableObjectsManager>
{
    [SerializeField] private PlacedItemsContainer container;
    [SerializeField] private Tilemap targetTileMap;

    public void Place(Item item, Vector3Int pos)
    {
        GameObject go = Instantiate(item.ItemPrefab);
        Vector3 position = targetTileMap.CellToWorld(pos) + targetTileMap.cellSize/2;
        // position += Vector3.forward * -0.1f;
        go.transform.position = position;

        container.PlacedItems.Add(new PlacedItem(item, go.transform, pos));
    }
}
