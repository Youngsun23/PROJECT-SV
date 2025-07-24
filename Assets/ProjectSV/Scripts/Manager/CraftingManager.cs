using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : SingletonBase<CraftingManager>
{
    private ItemContainer inventory; // 변경점: 플레이어의 모든 인벤(상자, 가방, ...) 모은 통합 인벤토리로 연결할 것
    private bool craftable = false;

    private void Start()
    {
        inventory = GameManager.Singleton.Inventory;
    }

    // 변경점: 1회 제작 -> 지정 개수만큼 제작(개수*배수 체크, 제작 개수*배수 Add)
    // OR 개수 지정 없이 제작 버튼만 있게, 꾹 눌러 여러 개 하거나
    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.IsFull()) return;

        if (!IsCraftable(recipe)) return;

        foreach(var required in recipe.Elements)
        {
            inventory.RemoveItem(required.Item, required.Count);
            inventory.AddItem(recipe.Output.Item, recipe.Output.Count);
        }
    }

    private bool IsCraftable(CraftingRecipe recipe)
    {
        foreach (var required in recipe.Elements)
        {
            int count = inventory.ItemSlots.Where(s => s.Item == required.Item).Sum(s => s.Count);

            if (count < required.Count) return false;
        }
        return true;
    }
}
