using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crop Data")]
public class CropData : GameDataBase
{
    public int FullGrowthTime => fullGrowthTime;
    public int GetGrowthStageTimer(int phase) { return growthStageTimer[phase]; }
    public Sprite GetGrowthSprite(int phase) { return growthSprites[phase]; }
    public int GetMaxGrowthStage() { return growthStageTimer.Count; }
    public Item GetYield() { return yield; }
    public int GetYieldCount() { return yieldCount; }

    [SerializeField] private int fullGrowthTime = 10;
    [SerializeField] private Item yield;
    [SerializeField] private int yieldCount = 1;

    [SerializeField] private List<Sprite> growthSprites;
    [SerializeField] private List<int> growthStageTimer;
}
