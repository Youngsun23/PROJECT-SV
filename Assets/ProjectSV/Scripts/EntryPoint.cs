using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Tilemap plowMap;
    [SerializeField] private Tilemap plantMap;
    [SerializeField] private Tilemap readMap;

    [SerializeField] private Collider2D confiner;

    private void Start()
    {
        if(plowMap != null)
            CropManager.Singleton.SetPlowTargetTileMap(plowMap);
        if(plantMap != null)
            CropManager.Singleton.SetPlantTargetTileMap(plantMap);
        if(readMap != null)
            TileMapReadManager.Singleton.SetReadTargetTileMap(readMap);

        CameraSystem.Singleton.SetCameraConfiner(confiner);
    }
}
