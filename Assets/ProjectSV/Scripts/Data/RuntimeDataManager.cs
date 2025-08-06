using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDataManager : SingletonBase<RuntimeDataManager>
{
    public List<CropTile> cropTiles = new List<CropTile>();
    public void UpdateRuntimeDataCropTilesData(List<CropTile> _cropTiles)
    {
        cropTiles = _cropTiles;
    }
    public List<CropTile> GetRuntimeDataCropTiles() => cropTiles;

    public List<PlacedItem> placedItems = new List<PlacedItem>();
    public void UpdateRuntimePlacedItemsData(List<PlacedItem> _placedItems)
    {
        placedItems = _placedItems;
    }
    public List<PlacedItem> GetRuntimeDataPlacedItems() => placedItems;
}
