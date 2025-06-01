using UnityEngine;

[CreateAssetMenu (menuName ="Data/Item Recipe")]
public class ItemRecipe : ScriptableObject
{
    public ItemType[] RecipeGrid => recipe3x3; 
    public Item ResultItem => resultItem;

    [SerializeField] private ItemType[] recipe3x3 = new ItemType[9];
    [SerializeField] private Item resultItem;
}
