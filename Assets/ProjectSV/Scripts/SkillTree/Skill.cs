using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTag
{
    MoveSpeed,
    ShippingPrice,
    QuestDay,
    Example,
    Example1,
    Example2,
    Example3,
    Example4,
    Example5,
    Example6,
    Example7,
    Example8,
    Example9,
}

[CreateAssetMenu(menuName = "Data/Skill")]
public class Skill : ScriptableObject
{
    public string Name => skillName;
    public SkillTag SkillTag => tag;
    public int MaxLevel => maxLevel;
    public Sprite Icon => icon;
    public string Info => info;
    public string ScalerUnit => scalerUnit;
    public int RequiredUnlockPoint => requiredUnlockPoint;

    [SerializeField] private string skillName;
    [SerializeField] private SkillTag tag;
    [OnValueChanged("AutoList")]
    [SerializeField] private int maxLevel;
    [SerializeField, ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)] private List<int> levelScaler; // 5%, +1
    [SerializeField, ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)] private List<int> levelUpCost; // 0->1(Unlock)
    [SerializeField] private Sprite icon;
    [SerializeField] private string info;
    [SerializeField] private string scalerUnit;
    [SerializeField] private List<Skill> childSkills;
    [SerializeField] private int requiredUnlockPoint;
    [SerializeField] private int ID; // Tree에서의 위치 파악용 - 십의 자리에는 위치한 행 번호, 일의 자리에는 열 번호

    public int GetScalerAtLevel(int level)
    {
        return levelScaler[level - 1];
    }

    public int GetCostAtLevel(int level)
    {
        return levelUpCost[level - 1];
    }

    public void UnlockChildSkill()
    {
        foreach(Skill child in childSkills)
        {
            UserDataManager.Instance.UpdateUserDataSkillUnlockPoint(child.SkillTag);
            
            if(child.RequiredUnlockPoint == UserDataManager.Instance.UserData.skillCurrentUnlockPointDictionary[child.SkillTag])
            {
                UnlockSkill();
            }
        }
    }

    // ToDo: UI 처리 어디서 할지 생각
    private void UnlockSkill()
    {
        // lockedSkillIcon.SetActive(false); <- skillButton(Tree)
        // levelUpButton.ButtonInteractableToggle(true); <- SkillPopup
    }

    public void AutoList()
    {
        if (levelScaler == null)
            levelScaler = new List<int>();
        if(levelUpCost == null)
            levelUpCost = new List<int>();

        if (levelScaler.Count < maxLevel)
        {
            while (levelScaler.Count < maxLevel)
                levelScaler.Add(0);
        }
        else if (levelScaler.Count > maxLevel)
        {
            levelScaler.RemoveRange(maxLevel, levelScaler.Count - maxLevel);
        }
        if (levelUpCost.Count < maxLevel)
        {
            while (levelUpCost.Count < maxLevel)
                levelUpCost.Add(0);
        }
        else if (levelUpCost.Count > maxLevel)
        {
            levelUpCost.RemoveRange(maxLevel, levelUpCost.Count - maxLevel);
        }
    }
}
