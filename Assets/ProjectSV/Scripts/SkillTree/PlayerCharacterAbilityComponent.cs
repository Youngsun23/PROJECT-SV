using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlayerCharacterAbilityComponent : MonoBehaviour
{
    public static PlayerCharacterAbilityComponent Instance;

    private UserDataDTO UserData => UserDataManager.Instance.UserData;

    public event Action OnLevelUp;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    [Button]
    public void TestLevelUp()
    {
        // 임시 코드 - MaxLevel 처리 필요
        if (UserDataManager.Instance.UserData.level == 10)
            return;
        UserDataManager.Instance.UpdateUserDataLevel();
        UserDataManager.Instance.UpdateUserDataSkillPoint(UserData.level);

        OnLevelUp?.Invoke();
    }

    public void AddEXP(int val)
    {
        UserDataManager.Instance.UpdateUserDataExp(val);
        if(UserData.exp >= GameDataManager.Instance.GetRequiredEXP(UserData.level))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        UserDataManager.Instance.UpdateUserDataExp(-GameDataManager.Instance.GetRequiredEXP(UserData.level));
        UserDataManager.Instance.UpdateUserDataLevel();
        UserDataManager.Instance.UpdateUserDataSkillPoint(UserData.level);

        OnLevelUp?.Invoke();
    }
}
