using System;

public class SkillTreePanel : SkillPanel
{
    protected override void Start()
    {
        base.Start();

        // �ӽ�: �ʱ⿡ �رݵǾ� �ִ� �� ���� ��ų�� -1->0���� ���� ���� ����
        // ToDo: ���������� ����&�ʱ�ȭ �ڵ� �۾� �Ϸ��� ��, UI �ڵ� ���Ű� �Բ� ��ųƮ�� ���� ���� �ʿ�
        TempInitialize();
    }

    public void TempInitialize()
    {
        for (int i = 0; i < 3; i++)
        {
            UserDataManager.Instance.UpdateUserDataSkillLevel(GameDataManager.Instance.skillGameData[i].SkillTag);
        }
    }

    public override void UIUpdate()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            try 
            {
                buttons[i].Set(GameDataManager.Instance.skillGameData[i].SkillTag);
            }
            catch (Exception) // ���ʿ� ��ų ���԰� ���ӵ����Ϳ� ���̰� ������ �ȵ�����, ���� �� ��ĭ ��ŵ��
            {
                buttons[i].SetDefault();
            }
        }
    }

    public override void OnClick(int id)
    {
        base.OnClick(id);

        SkillPopup.Instance.UIUpdate(GameDataManager.Instance.skillGameData[id].SkillTag);
    }
}
