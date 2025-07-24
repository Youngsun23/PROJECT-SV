using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Recipe Container")]
public class RecipeContainer : GameDataBase
{
    public List<CraftingRecipe> Recipes => recipes;
    [SerializeField] private List<CraftingRecipe> recipes;
}
