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
        // �ӽ� -> UserData Load�� ��ư �ƴ� GameManager �ڵ�� �ű�� ����, �ʱ�ȭ �ܰ迡�� ��� UI ����, UIUpdate()�� ���� �Ͱ� �и��� �� �����ϱ�
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

    // �÷��̾� ���� ���� -> ȣ�� (����=��� ������ ���� ��)
    public void UnlockBarSlot()
    {
        for (int i = 0; i < UserDataManager.Instance.UserData.level; i++)
        {
            buttons[i].UnlockSlot();
        }
    }
}
