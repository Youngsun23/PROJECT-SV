using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : SingletonBase<PlaceableObjectsManager>
{
    public PlacedItemsContainer Container => container;
    [SerializeField] private PlacedItemsContainer container;
    [SerializeField] private Tilemap targetTileMap;
    public bool isInitialized { get; private set; } = false;

    public void Initialize()
    {
        Debug.Log("PlaceableObjectsManager - Initialize() 호출");

        container.ClearPlacedItemsList();
        LoadPlacedItemsData();
        VisualizePlacedObjects();
    }

    public void InitilizeScene()
    {
        Debug.Log("PlaceableObjectsManager - InitializeScene() 호출");

        LoadRuntimePlacedItemsData();
        VisualizePlacedObjects();
    }

    public void SetInitialized(bool tf)
    {
        isInitialized = tf;
    }

    public void SavePlacedItemsData()
    {
        UserDataManager.Singleton.UpdateUserDataPlacedItems(container.PlacedItems);
    }

    public void LoadPlacedItemsData()
    {
        container.SetPlacedItemsList(UserDataManager.Singleton.GetUserDataPlacedItems());
        // Debug.Log($"LoadCropTilesData 호출 - {UserDataManager.Singleton.GetUserDataCropTiles().Count}");
    }

    public void SaveRuntimePlacedItemsData()
    {
        RuntimeDataManager.Singleton.UpdateRuntimePlacedItemsData(container.PlacedItems);
    }

    public void LoadRuntimePlacedItemsData()
    {
        container.SetPlacedItemsList(RuntimeDataManager.Singleton.GetRuntimeDataPlacedItems());
    }

    //private void Start()
    //{
    //    VisualizePlacedObjects();
    //}

    //private void OnDestroy()
    //{
    //    for (int i = 0; i < container.PlacedItems.Count; i++)
    //    {
    //        if (container.PlacedItems[i].Transform == null) continue;

    //        IPersistant persistant = container.PlacedItems[i].Transform.GetComponent<IPersistant>();
    //        if (persistant != null)
    //        {
    //            string jsonString = persistant.Read();
    //            container.PlacedItems[i].SetObjectState(jsonString);
    //        }

    //        container.PlacedItems[i].SetTransform(null);
    //    }
    //}

    public void VisualizePlacedObjects()
    {
        for(int i = 0; i < container.PlacedItems.Count; i++)
        {
            VisualizePlacedObject(container.PlacedItems[i]);
        }
    }

    private void VisualizePlacedObject(PlacedItem placedItem)
    {
        if (targetTileMap == null)
        {
            Debug.Log("targetTileMap == null");
            return;
        }

        GameObject go = Instantiate(placedItem.Item.ItemPrefab);
        Vector3 position = targetTileMap.CellToWorld(placedItem.Position) + targetTileMap.cellSize / 2;
        go.transform.position = position;
        placedItem.SetTransform(go.transform);
        // Debug.Log($"VisualizePlacedObject 함수 호출 - {placedItem.Position} -> {go.transform.position}");

        //IPersistant persistant = go.GetComponent<IPersistant>();
        //if(persistant != null)
        //{
        //    persistant.Load(placedItem.ObjectState);
        //}
    }

    public bool Place(Item item, Vector3Int pos)
    {
        if (container.PlacedItems.Find(x => x.Position == pos) != null) return false;
        
        PlacedItem placedItem = new PlacedItem(item, pos);
        VisualizePlacedObject(placedItem);
        container.PlacedItems.Add(placedItem);
        return true;
    }

    public void Pickup(Vector3Int pos)
    {
        PlacedItem placedItem = container.PlacedItems.Find(x => x.Position == pos);
        if (placedItem == null) return;

        ItemSpawnManager.Singleton.SpawnItem(targetTileMap.CellToWorld(pos), placedItem.Item, 1);
        Destroy(placedItem.Transform.gameObject);
        container.PlacedItems.Remove(placedItem);
    }

    public void SetTargetTileMap(Tilemap _targetTileMap)
    {
        targetTileMap = _targetTileMap;
    }
}
