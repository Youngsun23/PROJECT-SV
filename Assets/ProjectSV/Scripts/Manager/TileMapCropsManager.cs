using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class TileMapCropsManager : MonoBehaviour
{
    public PlantedCropsContainer Container => PlantedCropsManager.Singleton.Container;
    //[SerializeField] private PlantedCropsContainer container;

    // �� �� ���� �߰��ϸ� -> ���־� �ٽ� ����
    [SerializeField] private TileBase plowed;
    // [SerializeField] private TileBase watered;
    // [SerializeField] private TileBase default;
    // ���� ������ ������ ����? ��ĥ��
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

    private void Awake()
    {
        cropTargetTileMap = GetComponent<Tilemap>();
        CropManager.Singleton.SetCropsManager(this);
    }


    private void Start()
    {
        // CropManager.Singleton.SetCropsManager(this);
        // farmingTiles = new Dictionary<Vector3Int, CropTile>();

        // plowTargetTileMap = GetComponent<TileMap>();
        // plantTargetTileMap = GetComponent<TileMap>();
        // cropTargetTileMap = GetComponent<Tilemap>();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < Container.Crops.Count; i++)
        {
            Container.Crops[i].SetRenderer(null);
        }

        SaveRuntimeCropTilesData();
    }

    public void Initialize()
    {
        Debug.Log("TileMapCropsManager - Initialize() ȣ��");

        Container.ClearCropTileList();
        LoadCropTilesData(); 
        VisualizeTiles();
    }

    public void InitilizeScene()
    {
        Debug.Log("TileMapCropsManager - InitializeScene() ȣ��");

        LoadRuntimeCropTilesData();
        VisualizeTiles();
    }

    public void SaveCropTilesData()
    {
        UserDataManager.Singleton.UpdateUserDataCropTiles(Container.Crops);
    }

    public void LoadCropTilesData()
    {
        Container.SetCropTileList(UserDataManager.Singleton.GetUserDataCropTiles());
        // Debug.Log($"LoadCropTilesData ȣ�� - {UserDataManager.Singleton.GetUserDataCropTiles().Count}");
    }

    public void SaveRuntimeCropTilesData()
    {
        RuntimeDataManager.Singleton.UpdateRuntimeDataCropTilesData(Container.Crops);
    }

    public void LoadRuntimeCropTilesData()
    {
        Container.SetCropTileList(RuntimeDataManager.Singleton.GetRuntimeDataCropTiles());
    }

    public bool IsPlantable(Vector3Int pos)
    {
        //if (!farmingTiles.ContainsKey(pos) || farmingTiles[pos] != null)
        //    return false;
        if (Container.GetCropTile(pos) == null || Container.GetCropTile(pos).CropData != null)
            return false;
        else
            return true;
    }

    public void Plow(Vector3Int pos)
    {
        //if (farmingTiles.ContainsKey(pos))
        //    return;

        if(Container.GetCropTile(pos) != null)
            return;

        CreatePlowedTile(pos);
    }

    public void Plant(Vector3Int pos, CropData seed)
    {
        Container.GetCropTile(pos).SetCrop(seed);
        // farmingTiles[pos] = crop;

        VisualizeTile(Container.GetCropTile(pos));
    }

    public void VisualizeTiles()
    {
        foreach (var tile in Container.Crops)
        {
            VisualizeTile(tile);
        }
    }

    public void VisualizeTile(CropTile cropTile)
    {
        cropTargetTileMap.SetTile(cropTile.Position, plowed);

        if (cropTile.CropData != null)
        {
            // Debug.Log("VisualizeTile ȣ��: Renderer ����");
            GameObject cropSprite = Instantiate(cropSpritePrefab);
            cropSprite.transform.position = cropTargetTileMap.GetCellCenterWorld(cropTile.Position);

            // plantTargetTileMap.SetTile(pos, planted); // planted -> cropSpritePrefab�� �ش� seed�� 0��° �̹�����
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
        if (Container.GetCropTile(pos) == null) return;
        if (!Container.GetCropTile(pos).IsMature) return;

        //if (!farmingTiles.ContainsKey(pos)) return;
        //if (!farmingTiles[pos].isMature) return;

        Item yield = Container.GetCropTile(pos).CropData.GetYield();
        int yieldCount = Container.GetCropTile(pos).CropData.GetYieldCount();
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
        Container.Crops.Add(crop);
        // farmingTiles.Add(pos, null);

        cropTargetTileMap.SetTile(pos, plowed);
    }

    private void CreateHarvestedTile(Vector3Int pos)
    {
        Destroy(Container.GetCropTile(pos).Renderer.gameObject);
        // Destroy(farmingTiles[pos].renderer.gameObject);
        // farmingTiles.Remove(pos);
        Container.GetCropTile(pos).ResetCropTile();
        // farmingTiles[pos] = null;
        // plowTargetTileMap.SetTile(pos, null);
    }

    // Ÿ�� Ÿ�ϸ� - ���� �� �������� �����ҰŶ�� SetTargetTileMap() �߰��ؼ� �ذ�
    // �� ����� Start ���� ���� ����
    public void SetPlowTargetTileMap(Tilemap target)
    {
        plowTargetTileMap = target;
    }

    public void SetPlantTargetTileMap(Tilemap target)
    {
        plantTargetTileMap = target;
    }
}
