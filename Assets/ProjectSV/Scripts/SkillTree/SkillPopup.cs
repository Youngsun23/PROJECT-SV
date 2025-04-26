using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillPopup : MonoBehaviour
{
    public static SkillPopup Instance { get; private set; }

    private UserDataDTO UserData => UserDataManager.Instance.UserData;

    public Action onChange;

    [SerializeField] private SkillPopupIcon skillIcon;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image textHighlight;
    [SerializeField] private SkillPopupButton learnButton;
    [SerializeField] private SkillPopupButton levelUpButton;
    [SerializeField] private SkillPopupButton equipButton;
    [SerializeField] private SkillPopupButton unequipButton;

    private Skill selectedSkill;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        onChange += UIUpdate;
    }

    // Bar->Click �Ѿ�� ���
    public void UIUpdate(SkillTag tag)
    {
        selectedSkill = GameDataManager.Instance.GetSkillGameData(tag);
        int currentLevel = UserData.skillLevelDictionary[tag];

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
                infoText.text = $"{selectedSkill.Name}\n[{selectedSkill.Info}] +{selectedSkill.GetScalerAtLevel(currentLevel)}{selectedSkill.ScalerUnit} (���� ����: +{selectedSkill.GetScalerAtLevel(currentLevel + 1)}{selectedSkill.ScalerUnit})";
                levelUpButton.SetText($"Learn({selectedSkill.GetCostAtLevel(currentLevel)}P)");
            }
            else
            {
                infoText.text = $"{selectedSkill.Name}\n[{selectedSkill.Info}] +{selectedSkill.GetScalerAtLevel(currentLevel)}{selectedSkill.ScalerUnit} (Max)";
                levelUpButton.SetText("Max");
                levelUpButton.ButtonInteractableToggle(false);
            }
        }
        
        Debug.Log($"���õ� ��ų: {selectedSkill.Name} / ���� ����: {currentLevel} / 1���� ȿ��: {selectedSkill.GetScalerAtLevel(1)}");


        if (UserData.skillSet.Count < UserData.level && currentLevel >= 1)
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

    // Tree->Click �Ѿ�� ���
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
        // levelupCost �̻��� skillPoint �����ϰ� �ִ��� üũ
        int currentLevel = UserData.skillLevelDictionary[selectedSkill.SkillTag];
        int levelUpCost;

        if (currentLevel == 0)
        {
            levelUpCost = selectedSkill.GetCostAtLevel(1);
        }
        else
        {
            levelUpCost = selectedSkill.GetCostAtLevel(UserData.skillLevelDictionary[selectedSkill.SkillTag]);
        }
        if (UserData.skillPoint < levelUpCost)
        {
            levelUpButton.SetTextColorRed();
            return;
        }
        UserDataManager.Instance.UpdateUserDataSkillPoint(-levelUpCost);
        UserDataManager.Instance.UpdateUserDataSkillLevel(selectedSkill.SkillTag);

        // Learn: 0->1
        if (UserData.skillLevelDictionary[selectedSkill.SkillTag] == 1)
        {
            selectedSkill.UnlockChildSkill();
        }

        // ToDo: UI ���� - Bar, Tree, Popup �� �� ���� �����ؾ���
        onChange?.Invoke();
    }

    // ���/����
    public void EquipSkill()
    {
        UserData.skillSet.Add(selectedSkill.SkillTag);

        onChange?.Invoke();
    }

    public void UnequipSkill()
    {
        // SkillSet���� �ش� ��ų ����, UI ����
        UserData.skillSet.Remove(selectedSkill.SkillTag);

        onChange?.Invoke();
    }
}
