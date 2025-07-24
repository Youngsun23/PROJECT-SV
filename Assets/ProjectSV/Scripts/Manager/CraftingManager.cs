using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : SingletonBase<CraftingManager>
{
    private ItemContainer inventory; // ������: �÷��̾��� ��� �κ�(����, ����, ...) ���� ���� �κ��丮�� ������ ��
    private bool craftable = false;

    private void Start()
    {
        inventory = GameManager.Singleton.Inventory;
    }

    // ������: 1ȸ ���� -> ���� ������ŭ ����(����*��� üũ, ���� ����*��� Add)
    // OR ���� ���� ���� ���� ��ư�� �ְ�, �� ���� ���� �� �ϰų�
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
