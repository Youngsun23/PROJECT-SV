using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    protected List<SkillTag> activatedSkillSet => UserDataManager.Instance.UserData.skillSet;
    [SerializeField] protected List<SkillButton> buttons;
    private int previousSelectedButton;

    // ToDo: 다른 방식? SkillPopup에 있는 로직 함수를 분리하고 UI 스크립트에서는 호출만 하는 방식으로 변경 고려
    protected SkillPopup SkillUI => SkillPopup.Instance;

    protected virtual void Start()
    {
        SkillUI.onChange += UIUpdate;
        //Inventory.onChange += UIUpdate;
        Initialize();
    }

    public void Initialize()
    {
        SetIndex();
        UIUpdate();
    }

    private void SetIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void UIUpdate()
    {

    }

    public virtual void OnClick(int id)
    {
        // ToDo: 버튼 하이라이트가 Bar, Tree에 하나씩 남는 경우 해결 필요
        buttons[previousSelectedButton].Highlight(false);
        buttons[id].Highlight(true);
        previousSelectedButton = id;
    }
}
