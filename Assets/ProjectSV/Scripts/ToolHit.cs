using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public ItemSpawnManager ItemSpawnManager => GameManager.Instance.GetComponent<ItemSpawnManager>();

    public virtual void Hit()
    {

    }
}
