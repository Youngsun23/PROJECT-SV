using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CraftingSystem : SingletonBase<CraftingSystem>
{
    [SerializeField] private List<ItemRecipe> recipes;

    private Dictionary<string, ItemRecipe> recipeDictionary = new Dictionary<string, ItemRecipe>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var recipe in recipes)
        {
            string hash = GenerateCraftingHash(recipe.RecipeGrid);
            if (!recipeDictionary.ContainsKey(hash))
                recipeDictionary.Add(hash, recipe);
            else
                Debug.LogWarning($"Duplicate recipe hash: {hash}");
        }
    }

    public Item TryCraft(ItemType[] craftBoxGrid)
    {
        string hash = GenerateCraftingHash(craftBoxGrid);
        if (recipeDictionary.TryGetValue(hash, out ItemRecipe foundRecipe))
        {
            return foundRecipe.ResultItem;
        }
        return null;
    }

    private static string GenerateCraftingHash(ItemType[] grid)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in grid)
        {
            sb.Append((int)item).Append(",");
        }
        return sb.ToString();
    }

    public void ReturnUnusedItems()
    {
        foreach (var slot in CraftingSystemManager.Singleton.CraftBox.ItemSlots)
        {
            if (slot != null && slot.Item != null)
            {
                GameManager.Singleton.Inventory.AddItem(slot.Item, slot.Count);
                // �κ� �� ���� AddItem �����ϴ� ��� �ֺ��� ����ϴ� ��� �߰� ó�� �ʿ�
            }
        }
        CraftingSystemManager.Singleton.CraftBox.ClearContainer();
    }
}
