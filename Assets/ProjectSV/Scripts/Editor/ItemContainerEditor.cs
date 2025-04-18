using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;
        if(GUILayout.Button("Clear Container"))
        {
            for(int i = 0; i < container.ItemSlots.Count; i++)
            {
                container.ItemSlots[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}
