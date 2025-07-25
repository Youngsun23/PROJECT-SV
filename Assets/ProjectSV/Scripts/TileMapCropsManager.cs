using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class TileMapCropsManager : TimeAgent
{
    [SerializeField] private PlantedCropsContainer container;

    // 물 준 상태 추가하면 -> 비주얼 다시 조정
    [SerializeField] private TileBase plowed;
    // [SerializeField] private TileBase watered;
    // [SerializeField] private TileBase default;
    // 둘을 구분할 이유가 없나? 합칠까
    [SerializeField] private Tilemap cropTargetTileMap;
    [SerializeField] private Tilemap plowTargetTileMap;
    [SerializeField] private Tilemap plantTargetTileMap;
    // private Dictionary<Vector3Int, CropTile> farmingTiles;
    [SerializeField] private GameObject cropSpritePrefab;
    //private TimeAgent timeAgent;
    [SerializeField] private float spread = 2f;

    //private void Awake()
    //{
    //    timeAgent = GetComponent<TimeAgent>();
    //}


    protected override void Start()
    {
        base.Start();

        CropManager.Singleton.SetCropsManager(this);
        // farmingTiles = new Dictionary<Vector3Int, CropTile>();
        onTimeTick += Tick;
        // plowTargetTileMap = GetComponent<TileMap>();
        // plantTargetTileMap = GetComponent<TileMap>();
        cropTargetTileMap = GetComponent<Tilemap>();

        container.ClearCropTileList();
        LoadCropTilesData(); // 얠 어디로 보내야하나
        VisualizeTiles();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        onTimeTick -= Tick;

        for(int i = 0; i < container.Crops.Count; i++)
        {
            container.Crops[i].SetRenderer(null);
        }
    }

    public void Tick()
    {
        foreach (CropTile crop in container.Crops)
        {
            if (crop == null) continue;
            if (crop.CropData == null) continue;
            if (crop.IsMature) continue;

            crop.TickGrowthTimer(1);

            if (crop.CurrentGrowthTimer >= crop.CropData.GetGrowthStageTimer(crop.CurrentGrowthStage))
            {
                crop.TickGrowthStage(1);
                // Debug.Log($"Tick/CurrentTimer: {crop.currentGrowthTimer} // Tick/CurrentStage: {crop.currentGrowthStage}");
                crop.SetSprite(crop.CropData.GetGrowthSprite(crop.CurrentGrowthStage));

                if (crop.CurrentGrowthStage >= crop.CropData.GetMaxGrowthStage())
                {
                    crop.SetIsMature(true);
                    // Debug.Log($"{crop.cropData.name} 씨앗이 다 자랐습니다.");
                }
            }
        }
    }

    public void SaveCropTilesData()
    {
        UserDataManager.Singleton.UpdateUserDataCropTiles(container.Crops);
    }

    public void LoadCropTilesData()
    {
        container.SetCropTileList(UserDataManager.Singleton.GetUserDataCropTiles());
        // Debug.Log($"LoadCropTilesData 호출 - {UserDataManager.Singleton.GetUserDataCropTiles().Count}");
    }

    public bool IsPlantable(Vector3Int pos)
    {
        //if (!farmingTiles.ContainsKey(pos) || farmingTiles[pos] != null)
        //    return false;
        if (container.GetCropTile(pos) == null || container.GetCropTile(pos).CropData != null)
            return false;
        else
            return true;
    }

    public void Plow(Vector3Int pos)
    {
        //if (farmingTiles.ContainsKey(pos))
        //    return;

        if(container.GetCropTile(pos) != null)
            return;

        CreatePlowedTile(pos);
    }

    public void Plant(Vector3Int pos, CropData seed)
    {
        container.GetCropTile(pos).SetCrop(seed);
        // farmingTiles[pos] = crop;

        VisualizeTile(container.GetCropTile(pos));
    }

    public void VisualizeTiles()
    {
        foreach (var tile in container.Crops)
        {
            VisualizeTile(tile);
        }
    }

    public void VisualizeTile(CropTile cropTile)
    {
        cropTargetTileMap.SetTile(cropTile.Position, plowed);

        if (cropTile.CropData != null)
        {
            // Debug.Log("VisualizeTile 호출: Renderer 생성");
            GameObject cropSprite = Instantiate(cropSpritePrefab);
            cropSprite.transform.position = cropTargetTileMap.GetCellCenterWorld(cropTile.Position);

            // plantTargetTileMap.SetTile(pos, planted); // planted -> cropSpritePrefab을 해당 seed의 0번째 이미지로
            SpriteRenderer sr = cropSprite.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "WalkInfront";
            sr.sortingOrder = 2;
            cropTile.SetRenderer(sr);
            cropTile.SetSprite(cropTile.CropData.GetGrowthSprite(cropTile.CurrentGrowthStage));

            //Scene farmScene = SceneManager.GetSceneByName("Farm");
            //SceneManager.MoveGameObjectToScene(cropSprite, farmScene);
        }
    }

    public void PickupCrop(Vector3Int pos)
    {
        if (container.GetCropTile(pos) == null) return;
        if (!container.GetCropTile(pos).IsMature) return;

        //if (!farmingTiles.ContainsKey(pos)) return;
        //if (!farmingTiles[pos].isMature) return;

        Item yield = container.GetCropTile(pos).CropData.GetYield();
        int yieldCount = container.GetCropTile(pos).CropData.GetYieldCount();
        // Item yield = farmingTiles[pos].cropData.GetYield();
        // int yieldCount = farmingTiles[pos].cropData.GetYieldCount();
        Vector3 worldPos = cropTargetTileMap.GetCellCenterWorld(pos);
        worldPos.x += spread * Random.value * 2 - spread;
        worldPos.y += spread * Random.value * 2 - spread;
        ItemSpawnManager.Singleton.SpawnItem((Vector3)worldPos, yield, yieldCount);
        CreateHarvestedTile(pos);
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        if (cropTargetTileMap == null)
            return;

        CropTile crop = new CropTile(null, pos);
        container.Crops.Add(crop);
        // farmingTiles.Add(pos, null);

        cropTargetTileMap.SetTile(pos, plowed);
    }

    private void CreateHarvestedTile(Vector3Int pos)
    {
        Destroy(container.GetCropTile(pos).Renderer.gameObject);
        // Destroy(farmingTiles[pos].renderer.gameObject);
        // farmingTiles.Remove(pos);
        container.GetCropTile(pos).SetCrop(null);
        // farmingTiles[pos] = null;
        // plowTargetTileMap.SetTile(pos, null);
    }

    // 타겟 타일맵 - 농장 외 씬에서도 적용할거라면 SetTargetTileMap() 추가해서 해결
    // 이 방식은 Start 순서 문제 생김
    public void SetPlowTargetTileMap(Tilemap target)
    {
        plowTargetTileMap = target;
    }

    public void SetPlantTargetTileMap(Tilemap target)
    {
        plantTargetTileMap = target;
    }
}
