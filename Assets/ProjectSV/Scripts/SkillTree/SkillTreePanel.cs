using System;

public class SkillTreePanel : SkillPanel
{
    protected override void Start()
    {
        base.Start();

        // 임시: 초기에 해금되어 있는 세 개의 스킬만 -1->0으로 수동 레벨 조정
        // ToDo: 유저데이터 세팅&초기화 코드 작업 완료한 후, UI 자동 갱신과 함께 스킬트리 구조 변경 필요
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
            catch (Exception) // 애초에 스킬 슬롯과 게임데이터에 차이가 있으면 안되지만, 개발 중 빈칸 스킵용
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
