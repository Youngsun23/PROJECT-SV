using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public ItemSpawnManager ItemSpawnManager => GameManager.Instance.GetComponent<ItemSpawnManager>();

    public virtual void Hit()
    {

    }

    public virtual bool IsHittable(List<HitNodeType> hitNodeType)
    {
        return true;
    }
}
