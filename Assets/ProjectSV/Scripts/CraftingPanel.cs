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

    // 스듀 방식
    // 변경점: 아이템슬롯 클릭 -> 정보 오른편에 UI 세팅 -> 거기서 Craft 버튼 클릭 -> 아래 함수 실행
    //public override void OnClick(int id, bool isLeft)
    //{
    //    CraftingManager.Singleton.Craft(recipeContainer.Recipes[id]);
    //}
}
