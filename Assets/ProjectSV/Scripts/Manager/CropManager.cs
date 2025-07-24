using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CropTile
{
    public int currentGrowthTimer {  get; private set; }
    public CropData cropData {  get; private set; }
    public int currentGrowthStage { get; private set; }
    public bool isMature { get; private set; }   
    public SpriteRenderer renderer { get; private set; }
    public void SetRenderer(SpriteRenderer rd) {  renderer = rd; }
    public void SetSprite(Sprite sprite) { renderer.sprite = sprite; }  

    public void TickGrowthTimer(int time) {  this.currentGrowthTimer += time; }
    public void SetCrop(CropData crop) {  this.cropData = crop; }
    public void TickGrowthStage(int stage) { this.currentGrowthStage += stage; }
    public void SetIsMature(bool tf) { this.isMature = tf; }

    public CropTile(CropData _crop, int _timer = 0, int _stage = 0) { cropData = _crop; currentGrowthTimer = _timer;  currentGrowthStage = _stage; }
}

public class CropManager : SingletonBase<CropManager>
{
    // 물 준 상태 추가하면 -> 비주얼 다시 조정
    [SerializeField] private TileBase plowed;
    // [SerializeField] private TileBase watered;
    // [SerializeField] private TileBase default;
    [SerializeField] private Tilemap plowTargetTileMap;
    [SerializeField] private Tilemap plantTargetTileMap;
    private Dictionary<Vector3Int, CropTile> farmingTiles;
    [SerializeField] private GameObject cropSpritePrefab;
    private TimeAgent timeAgent;
    [SerializeField] private float spread = 2f;

    protected override void Awake()
    {
        base.Awake();
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
            if (crop == null) continue;
            if (crop.cropData == null) continue;
            if (crop.isMature) continue;

            crop.TickGrowthTimer(1);

            if(crop.currentGrowthTimer >= crop.cropData.GetGrowthStageTimer(crop.currentGrowthStage))
            {
                crop.TickGrowthStage(1);
                // Debug.Log($"Tick/CurrentTimer: {crop.currentGrowthTimer} // Tick/CurrentStage: {crop.currentGrowthStage}");
                crop.SetSprite(crop.cropData.GetGrowthSprite(crop.currentGrowthStage));

                if (crop.currentGrowthStage >= crop.cropData.GetMaxGrowthStage())
                {
                    crop.SetIsMature(true);
                    // Debug.Log($"{crop.cropData.name} 씨앗이 다 자랐습니다.");
                }
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
        sr.sortingLayerName = "WalkInfront";
        sr.sortingOrder = 2;
        crop.SetRenderer(sr);
        crop.SetSprite(seed.GetGrowthSprite(0));
    }

    public void PickupCrop(Vector3Int pos)
    {
        if (!farmingTiles.ContainsKey(pos)) return;
        if (!farmingTiles[pos].isMature) return;

        Item yield = farmingTiles[pos].cropData.GetYield();
        int yieldCount = farmingTiles[pos].cropData.GetYieldCount();
        Vector3 worldPos = plantTargetTileMap.GetCellCenterWorld(pos);
        worldPos.x += spread * Random.value * 2 - spread;
        worldPos.y += spread * Random.value * 2 - spread;
        ItemSpawnManager.Singleton.SpawnItem((Vector3)worldPos, yield, yieldCount);
        CreateHarvestedTile(pos);
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        farmingTiles.Add(pos, null);

        plowTargetTileMap.SetTile(pos, plowed);
    }

    private void CreateHarvestedTile(Vector3Int pos)
    {
        Destroy(farmingTiles[pos].renderer.gameObject);
        // farmingTiles.Remove(pos);
        farmingTiles[pos] = null;
        // plowTargetTileMap.SetTile(pos, null);
    }

    // 타겟 타일맵 - 농장 외 씬에서도 적용할거라면 SetTargetTileMap() 추가해서 해결
    public void SetPlowTargetTileMap(Tilemap target)
    {
        plowTargetTileMap = target;
    }

    public void SetPlantTargetTileMap(Tilemap target)
    {
        plantTargetTileMap = target;
    }
}
