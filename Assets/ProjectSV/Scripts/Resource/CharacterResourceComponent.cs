using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResourceComponent : MonoBehaviour
{
    public Dictionary<ResourceTypes, CharacterResource> resources = new Dictionary<ResourceTypes, CharacterResource>(); // �ε� -> ��ȭ �� �� ����, ���� (��Ÿ��) -> ����

    public void RegisterEvent(ResourceTypes type, System.Action<int> onChanged)
    {
        resources[type].OnChanged += onChanged;
    }

    public void EraseEvent(ResourceTypes type, System.Action<int> onChanged)
    {
        resources[type].OnChanged -= onChanged;
    }

    private void Awake()
    {
        for (int i = 0; i < (int)ResourceTypes.EndField; i++)
        {
            resources.Add((ResourceTypes)i, new CharacterResource());
        }
    }

    public void Initialize(List<CharacterResourceData> savedResource)
    {
        foreach(CharacterResourceData resource in savedResource)
        {
            resources[resource.Type].SetValue(resource.Count);
        }

        for(int i = 0; i < (int)ResourceTypes.EndField; i++)
        {
            resources[(ResourceTypes)i].OnChanged?.Invoke(resources[(ResourceTypes)i].Value);
        }
    }

    public void SaveCharacterResourceData()
    {
        List<CharacterResourceData> dataList = new List<CharacterResourceData>();
        foreach(var resource in resources)
        {
            CharacterResourceData data = new CharacterResourceData(resource.Key, resource.Value.Value);
            dataList.Add(data);
        }
        UserDataManager.Singleton.UpdateUserDataResource(dataList);
    }

    public CharacterResource GetResource(ResourceTypes type)
    {
        return resources[type];
    }

    public int GetResourceValue(ResourceTypes type)
    {
        return resources[type].Value;
    }

    public void SetResource(ResourceTypes type, int value)
    {
        resources[type].SetValue(value);

        resources[type].OnChanged?.Invoke(resources[type].Value);
    }

    public void ChangeResource(ResourceTypes type, int value)
    {
        int previousValue = resources[type].Value;
        resources[type].SetValue(previousValue + value);

        resources[type].OnChanged?.Invoke(resources[type].Value);
    }
}
