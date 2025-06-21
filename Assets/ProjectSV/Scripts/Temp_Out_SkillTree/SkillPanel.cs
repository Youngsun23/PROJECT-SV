//using System.Collections.Generic;
//using UnityEngine;

//public class SkillPanel : MonoBehaviour
//{
//    // protected List<SkillTag> activatedSkillSet => UserDataManager.Singleton.GetUserDataSkillSet();
//    [SerializeField] protected List<SkillButton> buttons;
//    private int previousSelectedButton;

//    protected SkillPopup SkillUI => SkillPopup.Singleton;

//    protected virtual void Start()
//    {
//        SkillUI.onChange += UIUpdate;
//        Initialize();
//    }

//    public void Initialize()
//    {
//        SetIndex();
//        UIUpdate();
//    }

//    private void SetIndex()
//    {
//        for (int i = 0; i < buttons.Count; i++)
//        {
//            buttons[i].SetIndex(i);
//        }
//    }

//    public virtual void UIUpdate()
//    {

//    }

//    public virtual void OnClick(int id)
//    {
//        buttons[previousSelectedButton].Highlight(false);
//        buttons[id].Highlight(true);
//        previousSelectedButton = id;
//    }
//}
