using HAD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : SingletonBase<ItemSpawnManager>
{
    [SerializeField] private GameObject itemPrefab;

    public void SpawnItem(Vector3 position, Item item, int count = 1)
    {
        GameObject obj = Instantiate(itemPrefab, position, Quaternion.identity);
        obj.GetComponent<PickableItem>().Set(item, count);
    }
}
