using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopupIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI levelText;

    public void Set(SkillTag skillTag)
    {
        Skill skill = GameDataManager.Instance.GetSkillGameData(skillTag);
        icon.sprite = skill.Icon;
        int currentLevel = UserDataManager.Instance.UserData.skillLevelDictionary[skillTag];

        if(currentLevel <= 0)
        {
            levelText.text = "";
        }
        else
        {
            levelText.text = currentLevel.ToString();
        }
        if (currentLevel == skill.MaxLevel)
        {
            levelText.color = Color.green;
        }

        icon.gameObject.SetActive(true);
    }

    public void SetDefault()
    {
        icon.gameObject.SetActive(false);
        levelText.text = "";
    }
}
