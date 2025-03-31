using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager Instance;

    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        Instance = this;    
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        GameObject obj = Instantiate(itemPrefab, position, Quaternion.identity);
        obj.GetComponent<PickableItem>().Set(item, count);
    }
}
