using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillPopup : SingletonBase<SkillPopup>
{
    private UserDataManager UserData => UserDataManager.Singleton;

    public Action onChange;

    [SerializeField] private SkillPopupIcon skillIcon;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image textHighlight;
    [SerializeField] private SkillPopupButton learnButton;
    [SerializeField] private SkillPopupButton levelUpButton;
    [SerializeField] private SkillPopupButton equipButton;
    [SerializeField] private SkillPopupButton unequipButton;

    private Skill selectedSkill;

    private void Start()
    {
        onChange += UIUpdate;
    }

    private void OnDestory()
    {
        onChange -= UIUpdate;
    }

    public void UIUpdate(SkillTag tag)
    {
        selectedSkill = GameDataManager.Singleton.GetSkillGameData(tag);
        int currentLevel = UserData.GetUserDataSkillLevelDictionary(tag);

        skillIcon.Set(tag);

        levelUpButton.ButtonInteractableToggle(true);

        if (currentLevel <= 0)
        {
            infoText.text = $"{selectedSkill.Name}\n[{selectedSkill.Info}] +{selectedSkill.GetScalerAtLevel(1)}{selectedSkill.ScalerUnit}";
            if (currentLevel < 0)
            {
                levelUpButton.ButtonInteractableToggle(false);
            }
            levelUpButton.SetText($"Learn({selectedSkill.GetCostAtLevel(1)}P)");
        }
        else
        {
            if (currentLevel != selectedSkill.MaxLevel)
            {
                infoText.text = $"{selectedSkill.Name}\n[{selectedSkill.Info}] +{selectedSkill.GetScalerAtLevel(currentLevel)}{selectedSkill.ScalerUnit} (다음 레벨: +{selectedSkill.GetScalerAtLevel(currentLevel + 1)}{selectedSkill.ScalerUnit})";
                levelUpButton.SetText($"Learn({selectedSkill.GetCostAtLevel(currentLevel)}P)");
            }
            else
            {
                infoText.text = $"{selectedSkill.Name}\n[{selectedSkill.Info}] +{selectedSkill.GetScalerAtLevel(currentLevel)}{selectedSkill.ScalerUnit} (Max)";
                levelUpButton.SetText("Max");
                levelUpButton.ButtonInteractableToggle(false);
            }
        }
        

        if (UserData.GetUserDataActivatedSkillSet().Count < UserData.GetUserDataLevel() && currentLevel >= 1)
        {
            equipButton.ButtonInteractableToggle(true);
        }
        else
        {
            equipButton.ButtonInteractableToggle(false);
        }
        
        textHighlight.gameObject.SetActive(true);
        learnButton.gameObject.SetActive(false);
        levelUpButton.gameObject.SetActive(true);
        equipButton.gameObject.SetActive(true);
        unequipButton.gameObject.SetActive(false);
    }

    public void UIUpdate()
    {
        UIUpdate(selectedSkill.SkillTag);
    }

    public void Clear()
    {
        skillIcon.SetDefault();
        infoText.text = "";
        textHighlight.gameObject.SetActive(false);
        learnButton.gameObject.SetActive(false);
        levelUpButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    public void LevelUpSkill()
    {
        int currentLevel = UserData.GetUserDataSkillLevelDictionary(selectedSkill.SkillTag);
        int levelUpCost;

        if (currentLevel == 0)
        {
            levelUpCost = selectedSkill.GetCostAtLevel(1);
        }
        else
        {
            levelUpCost = selectedSkill.GetCostAtLevel(UserData.GetUserDataSkillLevelDictionary(selectedSkill.SkillTag));
        }
        if (UserData.GetUserDataSkillPoint() < levelUpCost)
        {
            levelUpButton.SetTextColorRed();
            return;
        }
        UserDataManager.Singleton.UpdateUserDataSkillPoint(-levelUpCost);
        UserDataManager.Singleton.UpdateUserDataSkillLevel(selectedSkill.SkillTag);

        // Learn: 0->1
        if (UserData.GetUserDataSkillLevelDictionary(selectedSkill.SkillTag) == 1)
        {
            selectedSkill.UnlockChildSkill();
        }

        onChange?.Invoke();
    }

    public void EquipSkill()
    {
        UserData.UpdateUserDataSkillSet(selectedSkill.SkillTag, true);

        onChange?.Invoke();
    }

    public void UnequipSkill()
    {
        UserData.UpdateUserDataSkillSet(selectedSkill.SkillTag, false);

        onChange?.Invoke();
    }
}
