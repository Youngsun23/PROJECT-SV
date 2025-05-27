using HAD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int currentGrowthTimer {  get; private set; }
    public CropData cropData {  get; private set; }
    public int currentGrowthStage { get; private set; }
    public SpriteRenderer renderer { get; private set; }
    public void SetRenderer(SpriteRenderer rd) {  renderer = rd; }
    public void SetSprite(Sprite sprite) { renderer.sprite = sprite; }  

    public void TickGrowthTimer(int time) {  this.currentGrowthTimer += time; }
    public void SetCrop(CropData crop) {  this.cropData = crop; }
    public void TickGrowthStage(int stage) { this.currentGrowthStage += stage; }

    public CropTile(CropData _crop, int _timer = 0, int _stage = 0) { cropData = _crop; currentGrowthTimer = _timer;  currentGrowthStage = _stage; }
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
        foreach(CropTile crop in farmingTiles.Values)
        {
            if (crop == null) continue; // Values를 순회하는 상황에서 당연히 cropTile이 null인지를 먼저 체크 해야지 이 바보야~!~~!!~!~!!~~!~!~! 얘가 key가 아니고 value잖아!!!!!!
            if (crop.cropData == null) continue;
            if (crop.currentGrowthStage == crop.cropData.GetMaxGrowthStage()) continue;

            crop.TickGrowthTimer(1);

            if(crop.currentGrowthTimer >= crop.cropData.GetGrowthStageTimer(crop.currentGrowthStage))
            {
                crop.TickGrowthStage(1);
                Debug.Log($"Tick/CurrentTimer: {crop.currentGrowthTimer} // Tick/CurrentStage: {crop.currentGrowthStage}");
                crop.SetSprite(crop.cropData.GetGrowthSprite(crop.currentGrowthStage));

                if (crop.currentGrowthStage >= crop.cropData.GetMaxGrowthStage())
                    Debug.Log($"{crop.cropData.name} 씨앗이 다 자랐습니다.");
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
        CropTile crop = new CropTile(seed);
        farmingTiles[pos] = crop;

        GameObject cropSprite = Instantiate(cropSpritePrefab);
        cropSprite.transform.position = plantTargetTileMap.GetCellCenterWorld(pos);

        // plantTargetTileMap.SetTile(pos, planted); // planted -> cropSpritePrefab을 해당 seed의 0번째 이미지로
        SpriteRenderer sr = cropSprite.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Player";
        crop.SetRenderer(sr);
        crop.SetSprite(seed.GetGrowthSprite(0));
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        farmingTiles.Add(pos, null);

        plowTargetTileMap.SetTile(pos, plowed);
    }
}
