using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTickManager : TimeAgent
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
        if (PlantedCropsManager.Singleton.Container == null) return;

        foreach (CropTile crop in PlantedCropsManager.Singleton.Container.Crops)
        {
            if (crop == null) continue;
            if (crop.CropData == null) continue;
            if (crop.IsMature) continue;

            crop.TickGrowthTimer(1);

            if (crop.CurrentGrowthTimer >= crop.CropData.GetGrowthStageTimer(crop.CurrentGrowthStage))
            {
                crop.TickGrowthStage(1);
                // Debug.Log($"Tick/CurrentTimer: {crop.currentGrowthTimer} // Tick/CurrentStage: {crop.currentGrowthStage}");
                crop.SetSprite(crop.CropData.GetGrowthSprite(crop.CurrentGrowthStage));

                if (crop.CurrentGrowthStage >= crop.CropData.GetMaxGrowthStage())
                {
                    crop.SetIsMature(true);
                    // Debug.Log($"{crop.cropData.name} ¾¾¾ÑÀÌ ´Ù ÀÚ¶ú½À´Ï´Ù.");
                }
            }
        }
    }
}
