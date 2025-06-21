using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystemManager : SingletonBase<EquipmentSystemManager>
{
    public ItemContainer EquipmentBox => equipmentBox;
    [SerializeField] private ItemContainer origin_equipmentBox;
    [SerializeField] private ItemContainer equipmentBox;

    protected override void Awake()
    {
        base.Awake();

        equipmentBox = Instantiate(origin_equipmentBox);
    }
}
