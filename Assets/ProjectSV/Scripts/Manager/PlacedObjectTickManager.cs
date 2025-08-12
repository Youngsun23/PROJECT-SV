using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjectTickManager : TimeAgent
{
    protected override void Start()
    {
        base.Start();
        onTimeTick += Tick;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onTimeTick -= Tick;
    }

    public void Tick()
    {
        if (PlaceableObjectsManager.Singleton.Container == null) return;

        foreach (PlacedItem item in PlaceableObjectsManager.Singleton.Container.PlacedItems)
        {
            //if (item == null) continue;
            //if (item.ConvertorData == null) continue;
            //if (item?.ConvertorData?.ItemSlot?.Item == null) continue;
            //if (item.ConvertorData.IsConvertingOver) continue;
            if (item == null) continue;
            if (item.ConvertorData == null) continue;
            if (!item.ConvertorData.IsConverting) continue;

            item.ConvertorData.TickConvertingTimer(1);

            if (item.ConvertorData.CurrentTimer >= item.ConvertorData.FullTimer)
            {
                item.ConvertorData.SetIsConverting(false);
                item.ConvertorData.SetIsConvertingOver(true);
            }
        }
    }
}
