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
            // 기획 중: 경험치 -> 아이템화
            // PCAbilityComponent로 코드 넘기기(이 클래스는 데이터 GetSet만 있도록 압축해야 함)
        }
    }

    public void UpdateUserDataLevel()
    {
        UserData.level++;

        // ToDo: isMaxLevel 체크
        // PCAbilityComponent로 코드 넘기기(이 클래스는 데이터 GetSet만 있도록 압축해야 함)
    }

    public void UpdateUserDataSkillPoint(int val)
    {
        UserData.skillPoint += val;
    }

    public void UpdateUserDataSkillSet(int id, SkillTag tag) // 스킬셋 해제,등록,위치변경 시 -> 스킬셋 리스트 그대로 
    {
        UserData.skillSet[id] = tag;
    }

    public void UpdateUserDataSkillLevel(SkillTag tag) // 스킬 레벨 변경(레벨업)
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
    // DTO의 Set은 Manager만, 외부는 Manager의 Set만 사용
    // -> 불완전한 은닉화 택하느니 간결&편리가 낫다.
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
    public Dictionary<SkillTag, int> skillLevelDictionary = new Dictionary<SkillTag, int>(); // -1 해금x 0 해금o배움x 1~ 해금o배움o
    public Dictionary<SkillTag, int> skillCurrentUnlockPointDictionary = new Dictionary<SkillTag, int>();
    public List<SkillTag> skillSet = new List<SkillTag>(); // activatedskillset의 본체 -> 이거 그대로 ui에 보이게 세팅해줄거임

    public UserDataDTO()
    {
        //coin = 0;
        level = 1;
        exp = 0;
        skillPoint = 0;
        isMaxLevel = false;

        // 스킬 초기화 (레벨 0)
        foreach (SkillTag tag in Enum.GetValues(typeof(SkillTag)))
        {
            skillLevelDictionary[tag] = -1;
            skillCurrentUnlockPointDictionary[tag] = 0;
        }

        skillSet.Clear();
    }
}


