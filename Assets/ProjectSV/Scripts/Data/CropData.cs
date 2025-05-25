using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CropData")]
public class CropData : ScriptableObject
{
    public int GrowthTime => growthTime;
    public int GetGrowthStage(int phase) { return growthStageTimer[phase]; }
    public Sprite GetGrowthSprite(int phase) { return sprites[phase]; }

    [SerializeField] private int growthTime = 10;
    [SerializeField] private Item yeild;
    [SerializeField] private int count = 1;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<int> growthStageTimer;
}
