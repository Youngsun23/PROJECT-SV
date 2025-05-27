using HAD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growthTimer {  get; private set; }
    public CropData crop {  get; private set; }
    public int growthStage { get; private set; }
    public SpriteRenderer renderer { get; private set; }

    public void TickGrowthTimer(int time) {  this.growthTimer += time; }
    public void SetCrop(CropData crop) {  this.crop = crop; }
    public void TickGrowthStage(int stage) { this.growthStage += stage; }

    public CropTile(CropData _crop, int _timer = 0, int _stage = 0) { growthTimer = _timer; crop = _crop; growthStage = _stage; }
}

public class CropManager : SingletonBase<CropManager>
{
    [SerializeField] private TileBase plowed;
    [SerializeField] private TileBase planted;
    [SerializeField] private Tilemap plowTargetTileMap;
    [SerializeField] private Tilemap plantTargetTileMap;
    private Dictionary<Vector3Int, CropTile> farmingTiles;
    [SerializeField] private GameObject cropSpritePrefab;
    private TimeAgent timeAgent;

    protected override void Awake()
    {
        timeAgent = GetComponent<TimeAgent>();
    }

    private void OnDestroy()
    {
        timeAgent.onTimeTick -= Tick;
    }

    private void Start()
    {
        farmingTiles = new Dictionary<Vector3Int, CropTile>();
        timeAgent.onTimeTick += Tick;
    }

    public void Tick()
    {
        foreach(CropTile cropTile in farmingTiles.Values)
        {
            if (cropTile.crop == null) continue;

            cropTile.TickGrowthTimer(1);

            if(cropTile.growthTimer >= cropTile.crop.GrowthTime)
            {
                Debug.Log("Crop Fully Grown");
            }
        }
    }

    public bool IsPlantable(Vector3Int pos)
    {
        if (!farmingTiles.ContainsKey(pos) || farmingTiles[pos] != null)
            return false;
        else
            return true;
    }

    public void Plow(Vector3Int pos)
    {
        if (farmingTiles.ContainsKey(pos))
            return;

        CreatePlowedTile(pos);
    }

    public void Plant(Vector3Int pos, CropData seed)
    {
        GameObject go = Instantiate(cropSpritePrefab);
        go.transform.position = plantTargetTileMap.CellToWorld(pos);

        plantTargetTileMap.SetTile(pos, planted);

        CropTile crop = new CropTile(seed);
        farmingTiles[pos] = crop;
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        farmingTiles.Add(pos, null);

        plowTargetTileMap.SetTile(pos, plowed);
    }
}
