using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CropData")]
public class CropData : ScriptableObject
{
    public int FullGrowthTime => fullGrowthTime;
    public int GetGrowthStageTimer(int phase) { return growthStageTimer[phase]; }
    public Sprite GetGrowthSprite(int phase) { return growthSprites[phase]; }
    public int GetMaxGrowthStage() { return growthStageTimer.Count; }

    [SerializeField] private int fullGrowthTime = 10;
    [SerializeField] private Item yield;
    [SerializeField] private int yieldCount = 1;

    [SerializeField] private List<Sprite> growthSprites;
    [SerializeField] private List<int> growthStageTimer;
}
