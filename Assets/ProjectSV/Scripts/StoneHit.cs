using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHit : ToolHit
{
    [SerializeField] Item item;
    [SerializeField] int dropCount = 3;
    [SerializeField] float spread = 1f;

    public override void Hit()
    {
        for (int i = dropCount; i > 0; i--)
        {
            Vector3 position = transform.position;
            position.x += spread * Random.value * 2 - spread;
            position.y += spread * Random.value * 2 - spread;

            ItemSpawnManager.SpawnItem(position, item);
        }

        Destroy(gameObject);
    }
}
