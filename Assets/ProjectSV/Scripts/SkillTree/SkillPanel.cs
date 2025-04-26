using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    protected List<SkillTag> activatedSkillSet => UserDataManager.Instance.UserData.skillSet;
    [SerializeField] protected List<SkillButton> buttons;
    private int previousSelectedButton;

    // ToDo: �ٸ� ���? SkillPopup�� �ִ� ���� �Լ��� �и��ϰ� UI ��ũ��Ʈ������ ȣ�⸸ �ϴ� ������� ���� ���
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
        // ToDo: ��ư ���̶���Ʈ�� Bar, Tree�� �ϳ��� ���� ��� �ذ� �ʿ�
        buttons[previousSelectedButton].Highlight(false);
        buttons[id].Highlight(true);
        previousSelectedButton = id;
    }
}
