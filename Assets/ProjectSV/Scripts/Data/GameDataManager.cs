using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// GameManager [C] - 임시 해제

public partial class GameDataManager : SingletonBase<GameDataManager>
{
    public List<Skill> skillGameData = new List<Skill>();
    public List<int> expTable;

    public Skill GetSkillGameData(SkillTag tag)
    {
        return skillGameData.Find(x => x.SkillTag == tag);
    }

    public int GetRequiredEXP(int curLevel)
    {
        return expTable[curLevel + 1];
    }
}
