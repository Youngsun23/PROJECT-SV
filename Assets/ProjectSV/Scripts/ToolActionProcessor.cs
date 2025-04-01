using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Data/Tool Action/Tool Action Processor")]
public class ToolActionProcessor : ToolAction
{
    [SerializeField] private float interactableDistance = 1f;

    public override bool OnApply(Vector2 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, interactableDistance);
        foreach (Collider2D col in colliders)
        {
            ToolHit hit = col.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                return true;
            }
        }
        return false;
    }
}
