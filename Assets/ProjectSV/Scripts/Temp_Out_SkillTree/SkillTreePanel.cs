//using System;

//public class SkillTreePanel : SkillPanel
//{
//    protected override void Start()
//    {
//        base.Start();

//        TempInitialize();
//    }

//    public void TempInitialize()
//    {
//        for (int i = 0; i < 3; i++)
//        {
//            UserDataManager.Singleton.UpdateUserDataSkillLevel(GameDataManager.Singleton.skillGameData[i].SkillTag);
//        }
//    }

//    public override void UIUpdate()
//    {
//        for (int i = 0; i < buttons.Count; i++)
//        {
//            try 
//            {
//                buttons[i].Set(GameDataManager.Singleton.skillGameData[i].SkillTag);
//            }
//            catch (Exception)
//            {
//                buttons[i].SetDefault();
//            }
//        }
//    }

//    public override void OnClick(int id)
//    {
//        base.OnClick(id);

//        SkillPopup.Singleton.UIUpdate(GameDataManager.Singleton.skillGameData[id].SkillTag);
//    }
//}
