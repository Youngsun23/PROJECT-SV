using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 pos)
    {
        Debug.LogWarning("OnApply() Override Needed");
        return true;
    }
}