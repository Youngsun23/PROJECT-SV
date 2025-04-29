using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolBarController;

    private int previousSelectedTool;

    protected override void Start()
    {
        base.Start();
        toolBarController.onChange += HighlightButton;
        HighlightButton(0);
    }

    private void OnDestroy()
    {
        toolBarController.onChange -= HighlightButton;
    }

    public override void OnClick(int id)
    {
        toolBarController.SetPreviousSelectedTool(toolBarController.SelectedTool);
        toolBarController.SetSelectedTool(id);
        HighlightButton(id);
     //   previousSelectedTool = id;
    }

    public void HighlightButton(int id)
    {
        buttons[toolBarController.PreviousSelectedTool].Highlight(false);
        //buttons[previousSelectedTool].Highlight(false);
        buttons[toolBarController.SelectedTool].Highlight(true);
    }
}
