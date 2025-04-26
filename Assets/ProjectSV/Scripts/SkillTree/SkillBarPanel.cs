using TMPro;
using UnityEngine;

public class SkillBarPanel : SkillPanel
{
    [SerializeField] private TextMeshProUGUI spText; 

    protected override void Start()
    {
        base.Start();

        PlayerCharacterAbilityComponent.Instance.OnLevelUp += UIUpdate;
    }

    public override void UIUpdate()
    {
        // 임시 -> UserData Load를 버튼 아닌 GameManager 코드로 옮기고 나서, 초기화 단계에서 모든 UI 갱신, UIUpdate()에 남길 것과 분리할 것 구분하기
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

        spText.text = $"SP:{UserDataManager.Instance.UserData.skillPoint.ToString()}";
    }

    public override void OnClick(int id)
    {
        base.OnClick(id);

        SkillPopup.Instance.UIUpdate(activatedSkillSet[id]);
    }

    // 플레이어 레벨 세팅 -> 호출 (레벨=사용 가능한 슬롯 수)
    public void UnlockBarSlot()
    {
        for (int i = 0; i < UserDataManager.Instance.UserData.level; i++)
        {
            buttons[i].UnlockSlot();
        }
    }
}
