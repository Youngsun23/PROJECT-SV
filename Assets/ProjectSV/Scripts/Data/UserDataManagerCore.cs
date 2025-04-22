using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;
using Newtonsoft.Json;

public partial class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    [Button("Save")]
    public void Save()
    {
        // string serializeUserData = JsonUtility.ToJson(UserData, true); // Dictionary 직렬화 불가
        string serializeUserData = JsonConvert.SerializeObject(UserData, Formatting.Indented);

        FileUtility.WriteFileFromString("Assets/Resources/UserData.txt", serializeUserData);
    }

    [Button("Load")]
    public void Load()
    {
        if (FileUtility.ReadFileData("Assets/Resources/UserData.txt", out string loadedUserData))
        {
            UserData = JsonConvert.DeserializeObject<UserDataDTO>(loadedUserData);
        }
    }

    [Button("Delete")]
    public void Delete()
    {
        string filePath = "Assets/Resources/UserData.txt";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("User Data 삭제");
        }
    }
}
