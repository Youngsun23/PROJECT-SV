using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[Serializable]
public class CropTile
{
    public Vector3Int Position => position;
    public int CurrentGrowthTimer => currentGrowthTimer;
    public CropData CropData => cropData;
    public int CurrentGrowthStage => currentGrowthStage;
    public bool IsMature => isMature;
    public SpriteRenderer Renderer => renderer;


    [SerializeField] private Vector3Int position; // pos 정보를 Tile 안에 넣어버리기
    [SerializeField] private int currentGrowthTimer;
    [SerializeField] private CropData cropData;
    [SerializeField] private int currentGrowthStage;
    [SerializeField] private bool isMature;
    [SerializeField] private SpriteRenderer renderer;

    public void SetRenderer(SpriteRenderer rd) {  renderer = rd; }
    public void SetSprite(Sprite sprite) { renderer.sprite = sprite; }  
    public void TickGrowthTimer(int time) {  this.currentGrowthTimer += time; }
    public void SetCrop(CropData crop) {  this.cropData = crop; }
    public void TickGrowthStage(int stage) { this.currentGrowthStage += stage; }
    public void SetIsMature(bool tf) { this.isMature = tf; }

    public CropTile(CropData _crop, Vector3Int _pos, int _timer = 0, int _stage = 0) { cropData = _crop; position = _pos; currentGrowthTimer = _timer;  currentGrowthStage = _stage; }

    public void ResetCropTile()
    {
        cropData = null;
        currentGrowthTimer = 0;
        currentGrowthStage = 0;
        isMature = false;
        renderer = null;
    }
}

public class CropManager : SingletonBase<CropManager>
{
    public TileMapCropsManager TileMapCropManger => cropsManager;
    [SerializeField] private TileMapCropsManager cropsManager;

    private bool isInitialized = false;

    public void SetCropsManager(TileMapCropsManager manager)
    {
        cropsManager = manager;
        if(!isInitialized)
        {
            cropsManager.Initialize();
            isInitialized = true;
        }
        else
        {
            cropsManager.InitilizeScene();
        }
    }

    public void PickupCrop(Vector3Int pos)
    {
        if (cropsManager == null)
            return;

        cropsManager.PickupCrop(pos);
    }

    public bool IsPlantable(Vector3Int pos)
    {
        return cropsManager.IsPlantable(pos);
    }

    public void Plow(Vector3Int pos)
    {
        if (cropsManager == null)
            return;

        cropsManager.Plow(pos);
    }

    public void Plant(Vector3Int pos, CropData seed)
    {
        if (cropsManager == null)
            return;

        cropsManager.Plant(pos, seed);
    }

    public void SetPlowTargetTileMap(Tilemap target)
    {
        cropsManager.SetPlowTargetTileMap(target);
    }

    public void SetPlantTargetTileMap(Tilemap target)
    {
        cropsManager.SetPlantTargetTileMap(target);
    }
}

