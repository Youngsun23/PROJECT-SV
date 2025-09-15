using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// [RequireComponent(typeof(TimeAgent))]
public class ObjectSpawner : TimeAgent
{
    [SerializeField] private int spawnAreaHeight = 1;
    [SerializeField] private int spawnAreaWidth = 1;
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
            Destroy(gameObject);
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
        if (objectSpawnLimit <= spawnedObjectCount && objectSpawnLimit != -1) return;

        for (int i = 0; i < spawnCount; i++)
        {
            // int id = Random.Range(0, length);
            // GameObject go = Instantiate(objectToSpawn[id]);
            Item item = itemToSpawn[Random.Range(0, length)];

            Vector3Int pos = Vector3Int.RoundToInt(transform.position);
            pos.x += Random.Range(-spawnAreaWidth, spawnAreaWidth);
            pos.y += Random.Range(-spawnAreaHeight, spawnAreaHeight);
            // go.transform.position = pos;

            //if (!oneTime)
            //{
            //    go.transform.SetParent(this.transform);
            //    spawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
            //    spawnedObjects.Add(spawnedObject);
            //    spawnedObject.objID = id;
            //}

            if(PlaceableObjectsManager.Singleton.Place(item, pos))
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth * 2, spawnAreaHeight * 2));
    }
}
