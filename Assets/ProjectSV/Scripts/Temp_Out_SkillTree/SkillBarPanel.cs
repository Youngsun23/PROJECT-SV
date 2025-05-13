using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBarPanel : SkillPanel
{
    [SerializeField] private TextMeshProUGUI spText;

    private List<SkillTag> activatedSkillSet => UserDataManager.Singleton.GetUserDataActivatedSkillSet();

    protected override void Start()
    {
        base.Start();

        PlayerCharacterAbilityComponent.Singleton.OnLevelUp += UIUpdate;
    }

    private void OnDestroy()
    {
        PlayerCharacterAbilityComponent.Singleton.OnLevelUp -= UIUpdate;
    }

    public override void UIUpdate()
    {
        UnlockBarSlot();

        for (int i = 0; i < buttons.Count; i++)
        {
            if (activatedSkillSet.Count <= i)
            {
                buttons[i].SetDefault();
            }
            else
            {
                buttons[i].Set(activatedSkillSet[i]);
            }
        }

        spText.text = $"SP:{UserDataManager.Singleton.GetUserDataSkillPoint().ToString()}";
    }

    public override void OnClick(int id)
    {
        base.OnClick(id);

        SkillPopup.Singleton.UIUpdate(activatedSkillSet[id]);
    }

    public void UnlockBarSlot()
    {
        for (int i = 0; i < UserDataManager.Singleton.GetUserDataLevel(); i++)
        {
            buttons[i].UnlockSlot();
        }
    }
}
