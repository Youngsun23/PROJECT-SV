using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntryPoint : MonoBehaviour // ToDo: 별개의 클래스로 분리
{
    [SerializeField] private Tilemap plowMap;
    [SerializeField] private Tilemap plantMap;
    [SerializeField] private Tilemap readMap;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Tilemap placedItemMap;

    [SerializeField] private Collider2D confiner;

    // public event Action OnTilemapSet;

    private void Awake() // start?
    {
        if(plowMap != null)
            CropManager.Singleton.SetPlowTargetTileMap(plowMap);
        if(plantMap != null)
            CropManager.Singleton.SetPlantTargetTileMap(plantMap);
        if(readMap != null)
            TileMapReadManager.Singleton.SetReadTargetTileMap(readMap);
        if (highlightMap != null)
            GameManager.Singleton.PlaceableItemHighlight.SetTargetTileMap(highlightMap);
        if (placedItemMap != null)
        {
            PlaceableObjectsManager.Singleton.SetTargetTileMap(placedItemMap);
            if (!PlaceableObjectsManager.Singleton.isInitialized)
            {
                PlaceableObjectsManager.Singleton.Initialize();
                PlaceableObjectsManager.Singleton.SetInitialized(true);
            }
            else
            {
                PlaceableObjectsManager.Singleton.InitilizeScene();
            }
        }
    }

    private void Start()
    {
        if (confiner != null)
        {
            CameraSystem.Singleton.SetCameraConfiner(confiner);
        }
    }
}
