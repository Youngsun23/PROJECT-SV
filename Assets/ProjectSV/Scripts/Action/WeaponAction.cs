using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Weapon")]
public class WeaponAction : ToolAction
{
    public override bool OnApply(Vector2 pos, Item usedItem)
    {
        PlayerCharacter.Singleton.Attack(usedItem);

        return true;
    }
}
