using System;
using System.Collections.Generic;
using UnityEngine;

public partial class UserDataManager : MonoBehaviour
{
    public UserDataDTO UserData { get; private set; } = new UserDataDTO();

    public void UpdateUserDataExp(int value)
    {
        UserData.exp += value;

        if(UserData.isMaxLevel)
        {
            // ��ȹ ��: ����ġ -> ������ȭ
            // PCAbilityComponent�� �ڵ� �ѱ��(�� Ŭ������ ������ GetSet�� �ֵ��� �����ؾ� ��)
        }
    }

    public void UpdateUserDataLevel()
    {
        UserData.level++;

        // ToDo: isMaxLevel üũ
        // PCAbilityComponent�� �ڵ� �ѱ��(�� Ŭ������ ������ GetSet�� �ֵ��� �����ؾ� ��)
    }

    public void UpdateUserDataSkillPoint(int val)
    {
        UserData.skillPoint += val;
    }

    public void UpdateUserDataSkillSet(int id, SkillTag tag) // ��ų�� ����,���,��ġ���� �� -> ��ų�� ����Ʈ �״�� 
    {
        UserData.skillSet[id] = tag;
    }

    public void UpdateUserDataSkillLevel(SkillTag tag) // ��ų ���� ����(������)
    {
        UserData.skillLevelDictionary[tag]++;
    }

    public void UpdateUserDataSkillUnlockPoint(SkillTag tag)
    {
        UserData.skillCurrentUnlockPointDictionary[tag]++;
    }
}

[System.Serializable]
public class UserDataDTO
{
    // DTO�� Set�� Manager��, �ܺδ� Manager�� Set�� ���
    // -> �ҿ����� ����ȭ ���ϴ��� ����&���� ����.
    //public int Coin => coin;
    //public int Level => level;
    //public int EXP => exp;
    //public int SkillPoint => skillPoint;
    //public bool IsMaxLevel => isMaxLevel;
    //public int GetSkillLevelDictionary(SkillTag skillTag)
    //{
    //    return skillLevelDictionary[skillTag];
    //}
    //public SkillTag GetSkillSet(int id)
    //{
    //    return skillSet[id];
    //}

    //public void SetExp(int val) => exp = val;
    //public void AddExp(int val) => exp += val;
    //public void SetCoin(int val) => coin = val;
    //public void AddCoin(int val) => coin += val;

    //public int coin;
    public int level;
    public int exp;
    public int skillPoint;
    public bool isMaxLevel;
    public Dictionary<SkillTag, int> skillLevelDictionary = new Dictionary<SkillTag, int>(); // -1 �ر�x 0 �ر�o���x 1~ �ر�o���o
    public Dictionary<SkillTag, int> skillCurrentUnlockPointDictionary = new Dictionary<SkillTag, int>();
    public List<SkillTag> skillSet = new List<SkillTag>(); // activatedskillset�� ��ü -> �̰� �״�� ui�� ���̰� �������ٰ���

    public UserDataDTO()
    {
        //coin = 0;
        level = 1;
        exp = 0;
        skillPoint = 0;
        isMaxLevel = false;

        // ��ų �ʱ�ȭ (���� 0)
        foreach (SkillTag tag in Enum.GetValues(typeof(SkillTag)))
        {
            skillLevelDictionary[tag] = -1;
            skillCurrentUnlockPointDictionary[tag] = 0;
        }

        skillSet.Clear();
    }
}


