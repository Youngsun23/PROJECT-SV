using System;
using System.Collections.Generic;
using UnityEditor.iOS;

public partial class UserDataManager : SingletonBase<UserDataManager>
{
    private UserDataDTO UserData = new UserDataDTO();

    public void UpdateUserDataResource(Dictionary<ResourceTypes, CharacterResource> resources)
    {
        UserData.resource = resources;
    }

    public void UpdateUserDataExp(int value)
    {
        UserData.exp += value;

        if(UserData.isMaxLevel)
        {
        }
    }

    public void UpdateUserDataLevel()
    {
        UserData.level++;
    }

    //public void UpdateUserDataSkillPoint(int val)
    //{
    //    UserData.skillPoint += val;
    //}

    //public void UpdateUserDataSkillSet(SkillTag tag, bool equip) 
    //{
    //    if(equip)
    //        UserData.equippedSkillSet.Add(tag);
    //    else
    //        UserData.equippedSkillSet.Remove(tag);
    //}

    //public void UpdateUserDataSkillLevel(SkillTag tag) 
    //{
    //    UserData.skillLevelDictionary[tag]++;
    //}

    //public void UpdateUserDataSkillUnlockPoint(SkillTag tag)
    //{
    //    UserData.skillCurrentUnlockPointDictionary[tag]++;
    //}

    public void UpdateUserDataEquipment(EquipItem item, bool equip)
    {
        if(equip)
            UserData.equippedItems.Add(item);
        else
            UserData.equippedItems.Remove(item);
    }

    public void UpdateUserDataEquipmentAtOnce(ItemContainer equipBox)
    {
        UserData.equippedItems.Clear();

        foreach (ItemSlot slot in equipBox.ItemSlots)
        {
            if(slot.Item != null)
            {
                UserData.equippedItems.Add(slot.Item as EquipItem);
            }
        }
    }

    public void UpdateUserDataCropTiles(List<CropTile> _cropTiles)
    {
        UserData.cropTiles = _cropTiles;
    }

    public void UpdateUserDataPlacedItems(List<PlacedItem> _placedItems)
    {
        UserData.placedItems = _placedItems;
    }

    public Dictionary<ResourceTypes, CharacterResource> GetUserDataResource() => UserData.resource;
    public int GetUserDataLevel() => UserData.level;
    public int GetUserDataEXP() => UserData.exp;
    public int GetUserDataSkillPoint() => UserData.skillPoint;
    public bool GetUserDataIsMaxLevel() => UserData.isMaxLevel;

    public List<EquipItem> GetUserDataEquippedItems() => UserData.equippedItems;
    //public int GetUserDataSkillLevelDictionary(SkillTag tag) => UserData.skillLevelDictionary[tag];
    //public int GetUserDataSkillCurrentUnlockPointDictionary(SkillTag tag) => UserData.skillCurrentUnlockPointDictionary[tag];
    //public List<SkillTag> GetUserDataActivatedSkillSet() => UserData.equippedSkillSet;

    public List<CropTile> GetUserDataCropTiles() => UserData.cropTiles;
    public List<PlacedItem> GetUserDataPlacedItems() => UserData.placedItems;
}

[System.Serializable]
public class UserDataDTO
{
    public Dictionary<ResourceTypes, CharacterResource> resource;
    public int level;
    public int exp;
    public int skillPoint;
    public bool isMaxLevel;

    public List<EquipItem> equippedItems = new List<EquipItem>();
    //public Dictionary<SkillTag, int> skillLevelDictionary = new Dictionary<SkillTag, int>();
    //public Dictionary<SkillTag, int> skillCurrentUnlockPointDictionary = new Dictionary<SkillTag, int>();
    //public List<SkillTag> equippedSkillSet = new List<SkillTag>();

    public List<CropTile> cropTiles = new List<CropTile>();
    public List<PlacedItem> placedItems = new List<PlacedItem>();

    public UserDataDTO()
    {
        level = 1;
        exp = 0;
        skillPoint = 0;
        isMaxLevel = false;

        resource.Clear();
        equippedItems.Clear();
        //foreach (SkillTag tag in Enum.GetValues(typeof(SkillTag)))
        //{
        //    skillLevelDictionary[tag] = -1;
        //    skillCurrentUnlockPointDictionary[tag] = 0;
        //}
        //equippedSkillSet.Clear();

        cropTiles.Clear();
        placedItems.Clear();
    }
}


