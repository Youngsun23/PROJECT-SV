using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] private GameObject ToolBarCanvas;

    [SerializeField] int toolBarSize = 10;
    public int SelectedTool => selectedTool;
    private int selectedTool;
    public int PreviousSelectedTool => previousSelectedTool;    
    private int previousSelectedTool;

    public Action<int> onChange;

    private void Update()
    {
        if (!ToolBarCanvas.activeSelf)
            return;

        float delta = Mouse.current.scroll.ReadValue().y;
        if (delta != 0)
        {
            if(delta < 0)
            {
                previousSelectedTool = selectedTool++;
                selectedTool = (selectedTool > toolBarSize - 1 ?  0 : selectedTool);
            }
            else
            {
                previousSelectedTool = selectedTool--;
                selectedTool = (selectedTool < 0 ? toolBarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    public void SetSelectedTool(int id)
    {
        selectedTool = id;
    }
    public void SetPreviousSelectedTool(int id)
    {
        previousSelectedTool = id;
    }

    public Item GetCurrentHoldingItem()
    {
        return GameManager.Singleton.Inventory.ItemSlots[selectedTool].Item;
    }
}
