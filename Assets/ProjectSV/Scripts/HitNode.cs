using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitNode : ToolHit
{
    [SerializeField] private Item item;
    [SerializeField] private int dropCount = 3;
    [SerializeField] private float spread = 1f;
    [SerializeField] private HitNodeType nodeType;

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

    public override bool IsHittable(List<HitNodeType> hitNodeType)
    {
        return hitNodeType.Contains(nodeType);   
    }
}
