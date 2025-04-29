using HAD;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class UserDataManager : SingletonBase<UserDataManager>
{
    // public UserDataDTO UserData { get; private set; } = new UserDataDTO();
    private UserDataDTO UserData = new UserDataDTO();

    public void UpdateUserDataExp(int value)
    {
        UserData.exp += value;

        if(UserData.isMaxLevel)
        {
        }
    }

    public void UpdateUserDataLevel()
    {
        UserData.level++;
    }

    public void UpdateUserDataSkillPoint(int val)
    {
        UserData.skillPoint += val;
    }

    public void UpdateUserDataSkillSet(SkillTag tag, bool equip) 
    {
        if(equip)
            UserData.equippedSkillSet.Add(tag);
        else
        UserData.equippedSkillSet.Remove(tag);
    }

    public void UpdateUserDataSkillLevel(SkillTag tag) 
    {
        UserData.skillLevelDictionary[tag]++;
    }

    public void UpdateUserDataSkillUnlockPoint(SkillTag tag)
    {
        UserData.skillCurrentUnlockPointDictionary[tag]++;
    }

    public int GetUserDataLevel() => UserData.level;
    public int GetUserDataEXP() => UserData.exp;
    public int GetUserDataSkillPoint() => UserData.skillPoint;
    public bool GetUserDataIsMaxLevel() => UserData.isMaxLevel;
    public int GetUserDataSkillLevelDictionary(SkillTag tag) => UserData.skillLevelDictionary[tag];
    public int GetUserDataSkillCurrentUnlockPointDictionary(SkillTag tag) => UserData.skillCurrentUnlockPointDictionary[tag];
    public List<SkillTag> GetUserDataActivatedSkillSet() => UserData.equippedSkillSet;
}

[System.Serializable]
public class UserDataDTO
{
    //public int coin;
    public int level;
    public int exp;
    public int skillPoint;
    public bool isMaxLevel;
    public Dictionary<SkillTag, int> skillLevelDictionary = new Dictionary<SkillTag, int>();
    public Dictionary<SkillTag, int> skillCurrentUnlockPointDictionary = new Dictionary<SkillTag, int>();
    public List<SkillTag> equippedSkillSet = new List<SkillTag>();

    public UserDataDTO()
    {
        //coin = 0;
        level = 1;
        exp = 0;
        skillPoint = 0;
        isMaxLevel = false;

        foreach (SkillTag tag in Enum.GetValues(typeof(SkillTag)))
        {
            skillLevelDictionary[tag] = -1;
            skillCurrentUnlockPointDictionary[tag] = 0;
        }

        equippedSkillSet.Clear();
    }
}


