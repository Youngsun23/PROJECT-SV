using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

// [RequireComponent(typeof(TimeAgent))]
public class ObjectSpawner : TimeAgent
{
    [SerializeField] private float spawnAreaHeight = 1;
    [SerializeField] private float spawnAreaWidth = 1;
    [SerializeField] private GameObject[] objectToSpawn;
    [SerializeField] private Item[] itemToSpawn;
    private int length;
    [SerializeField] private float probability = 0.1f;
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private bool oneTime = false;
    [SerializeField] private int objectSpawnLimit = -1;
    [SerializeField] private int spawnedObjectCount = 0;
    //List<SpawnedObject> spawnedObjects;
    //[SerializeField] private JSONStringList targetSaveJSONList;
    //[SerializeField] int idInList = -1;

    // Gizmos용
    public Tilemap targetTilemap;

    protected override void Start()
    {
        length = itemToSpawn.Length;
        if (!oneTime)
        {
            base.Start();
            onTimeTick += Tick;
            //spawnedObjects = new List<SpawnedObject>;
            //LoadData();
        }
        else
        {
            Tick();
            // Destroy(gameObject);
        }
    }

    protected override void OnDestroy()
    {
        if (!oneTime)
        {
            base.OnDestroy();
            onTimeTick -= Tick;

            //SaveData();
        }
    }

    public void Tick()
    {
        if (Random.value > probability) return;
        if (objectSpawnLimit != -1 && objectSpawnLimit <= spawnedObjectCount) return;

        for (int i = 0; i < spawnCount; i++)
        {
            // int id = Random.Range(0, length);
            // GameObject go = Instantiate(objectToSpawn[id]);
            Item item = itemToSpawn[Random.Range(0, length)];

            //Vector3Int pos = Vector3Int.RoundToInt(transform.position);
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnAreaWidth, spawnAreaWidth);
            pos.y += Random.Range(-spawnAreaHeight, spawnAreaHeight);
            Vector3Int cellPos = TileMapReadManager.Singleton.TargetMap.WorldToCell(pos);
            // go.transform.position = pos;

            //if (!oneTime)
            //{
            //    go.transform.SetParent(this.transform);
            //    spawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
            //    spawnedObjects.Add(spawnedObject);
            //    spawnedObject.objID = id;
            //}

            if(PlaceableObjectsManager.Singleton.Place(item, cellPos))
            {
                spawnedObjectCount++;
            }
        }
    }

    //public void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    //{

    //}
    //public class ToSave
    //{
    //    public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;

    //    public ToSave()
    //    {
    //        spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
    //    }
    //}
    //public void Read()
    //{
    //    ToSave toSave = new ToSave();

    //    for(int i = 0; i < spawnedObjects.Count; i++)
    //    {
    //        toSave.spawnedObjectDatas.Add(new SpawnedObject.SaveSpawnedObjectData(spawnedObjects[i].objID, spawnedObjects[i].transform.position));
    //    }

    //    return JsonUtility.ToJson(toSave);
    //}
    //public void Load(string json)
    //{
    //    if(json == "" || json == "{}" || json == null)
    //        return;

    //    ToSave toLoad = JsonUtility.FromJson<ToSave>(json);       

    //    for(int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
    //    {
    //        SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
    //        GameObject go = Instnatiate(objectToSpawn[data.objectID]);
    //        go.transform.position = data.worldPosition;
    //        go.transform.SetParent(this.transform);
    //        SpawnedObject so = go.AddComponent<SpawnedObject>();
    //        so.objID = data.objectID;
    //        spawnedObjects.Add(so);
    //    }
    //}
    //private void SaveData()
    //{
    //    if(!CheckJSON()) return;
    //    string jsonString = Read();
    //    targetSaveJSONList.SetString(jsonString, idInList);
    //}
    //private void LoadData()
    //{
    //    if (!CheckJSON()) return;
    //    LoadData(targetSaveJSONList.GetString(idInList));
    //}
    //private bool CheckJSON()
    //{
    //    if (oneTime) return false;
    //    if (targetSaveJSONList == null)
    //    {
    //        Debug.LogError("target json scriptable object to save data on spawned objects is null.");
    //        return false;
    //    }
    //    if (idInList == -1)
    //    {
    //        Debug.LogError("id in list is not assigned. Data can't be saved.");
    //        return false;
    //    }
    //    return true;
    //}

    // 버전1
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth * 2, spawnAreaHeight * 2));
    //}

    // 버전2
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // 월드 좌표로 스폰 범위 구하기
        Vector3 minWorld = transform.position - new Vector3(spawnAreaWidth, spawnAreaHeight, 0f);
        Vector3 maxWorld = transform.position + new Vector3(spawnAreaWidth, spawnAreaHeight, 0f);

        // 월드 → 셀 변환
        Vector3Int minCell = targetTilemap.WorldToCell(minWorld);
        Vector3Int maxCell = targetTilemap.WorldToCell(maxWorld);

        // 셀 범위를 월드 좌표로 다시 변환
        Vector3 snappedMin = targetTilemap.CellToWorld(minCell);
        Vector3 snappedMax = targetTilemap.CellToWorld(maxCell + Vector3Int.one);

        // 셀 범위의 중심과 크기 계산
        Vector3 center = (snappedMin + snappedMax) / 2f;
        Vector3 size = snappedMax - snappedMin;

        // 셀 단위로 스냅된 박스 그리기
        Gizmos.DrawWireCube(center, size);
    }
}
