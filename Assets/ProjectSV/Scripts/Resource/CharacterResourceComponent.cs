using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceTypes
{
    Coin,
    // 이런저런 값 하나짜리 재화들
    SpringOrb,
    SummerOrb,
    FallOrb,
    WinterOrb,

    EndField,
}


public class CharacterResourceComponent : MonoBehaviour
{
    public Dictionary<ResourceTypes, CharacterResource> resources = new Dictionary<ResourceTypes, CharacterResource>(); // 로드 -> 재화 별 값 저장, 변경 (런타임) -> 저장

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

    public void Initialize(Dictionary<ResourceTypes, CharacterResource> savedResource)
    {
        resources = savedResource;

        for(int i = 0; i < (int)ResourceTypes.EndField; i++)
        {
            resources[(ResourceTypes)i].OnChanged?.Invoke(resources[(ResourceTypes)i].Value);
        }
    }

    public CharacterResource GetResource(ResourceTypes type)
    {
        return resources[type];
    }

    public float GetResourceValue(ResourceTypes type)
    {
        return resources[type].Value;
    }

    public void SetResource(ResourceTypes type, int value)
    {
        resources[type].Value = value;

        resources[type].OnChanged?.Invoke(resources[type].Value);
    }

    public void ChangeResource(ResourceTypes type, int value)
    {
        resources[type].Value += value;

        resources[type].OnChanged?.Invoke(resources[type].Value);
    }
}
