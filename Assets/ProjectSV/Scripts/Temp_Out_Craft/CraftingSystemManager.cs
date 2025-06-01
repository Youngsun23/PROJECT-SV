using HAD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemManager : SingletonBase<CraftingSystemManager>
{
    public ItemContainer CraftBox => craftBox;
    [SerializeField] private ItemContainer origin_craftBox;
    [SerializeField] private ItemContainer craftBox;

    protected override void Awake()
    {
        craftBox = Instantiate(origin_craftBox);
    }
}
