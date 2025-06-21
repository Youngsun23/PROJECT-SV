//using System;
//using UnityEngine;

//// PlayerCharacter [C] - 임시 해제
//public class PlayerCharacterAbilityComponent : SingletonBase<PlayerCharacterAbilityComponent>
//{
//    private UserDataManager UserData => UserDataManager.Singleton;

//    public event Action OnLevelUp;

//    // [Button]
//    public void TestLevelUp()
//    {
//        if (UserData.GetUserDataLevel() == 10)
//            return;
//        UserData.UpdateUserDataLevel();
//        UserData.UpdateUserDataSkillPoint(UserData.GetUserDataLevel());

//        OnLevelUp?.Invoke();
//    }

//    public void AddEXP(int val)
//    {
//        UserData.UpdateUserDataExp(val);
//        if(UserData.GetUserDataEXP() >= GameDataManager.Singleton.GetRequiredEXP(UserData.GetUserDataLevel()))
//        {
//            LevelUp();
//        }
//    }

//    public void LevelUp()
//    {
//        UserDataManager.Singleton.UpdateUserDataExp(-GameDataManager.Singleton.GetRequiredEXP(UserData.GetUserDataLevel()));
//        UserDataManager.Singleton.UpdateUserDataLevel();
//        UserDataManager.Singleton.UpdateUserDataSkillPoint(UserData.GetUserDataLevel());

//        OnLevelUp?.Invoke();
//    }
//}
