using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameDataManager : MonoBehaviour
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
