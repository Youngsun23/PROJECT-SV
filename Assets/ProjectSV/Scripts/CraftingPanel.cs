using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingPanel : ItemPanel
{
    [SerializeField] private RecipeContainer recipeContainer;

    public override void Show()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Set(recipeContainer.Recipes[i].Output);
        }
    }

    // ���� ���
    // ������: �����۽��� Ŭ�� -> ���� ������ UI ���� -> �ű⼭ Craft ��ư Ŭ�� -> �Ʒ� �Լ� ����
    //public override void OnClick(int id, bool isLeft)
    //{
    //    CraftingManager.Singleton.Craft(recipeContainer.Recipes[id]);
    //}
}
