using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHit : ToolHit
{
    [SerializeField] GameObject pickableDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 1f;

    public override void Hit()
    {
        for(int i = dropCount; i > 0; i--)
        {
            Vector3 position = transform.position;
            position.x += spread * Random.value * 2 - spread;
            position.y += spread * Random.value * 2 - spread;
            GameObject drop = Instantiate(pickableDrop);
            drop.transform.position = position;
        }

        Destroy(gameObject);
    }
}
