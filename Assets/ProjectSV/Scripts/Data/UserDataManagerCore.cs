using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public partial class UserDataManager : SingletonBase<UserDataManager>
{
    // [Button("Save")]
    public void Save()
    {
        string serializeUserData = JsonUtility.ToJson(UserData, true);
        // string serializeUserData = JsonConvert.SerializeObject(UserData, Formatting.Indented);

        FileUtility.WriteFileFromString("Assets/Resources/UserData.txt", serializeUserData);
    }

    // [Button("Load")]
    public void Load()
    {
        if (FileUtility.ReadFileData("Assets/Resources/UserData.txt", out string loadedUserData))
        {
            UserData = JsonUtility.FromJson<UserDataDTO>(loadedUserData);
            PlayerCharacter.Singleton.InitializeCharacterAttribute();
        }
    }

    // [Button("Delete")]
    public void Delete()
    {
        string filePath = "Assets/Resources/UserData.txt";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    // 임시
    public void BuildSave()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "UserData.txt");
        string serializeUserData = JsonUtility.ToJson(UserData, true);
        FileUtility.WriteFileFromString(filePath, serializeUserData);
    }

    public void BuildLoad()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "UserData.txt");
        if (FileUtility.ReadFileData(filePath, out string loadedUserData))
        {
            UserData = JsonUtility.FromJson<UserDataDTO>(loadedUserData);
            PlayerCharacter.Singleton.InitializeCharacterAttribute();
        }
    }
}

