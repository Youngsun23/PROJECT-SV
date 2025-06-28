using UnityEngine;

public class UITabController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject toolBarCanvas;
    [SerializeField] private ItemPanel toolBarPanel;
    [SerializeField] private GameObject dragDropIcon;
    // 스킬트리 임시 해제
    //[SerializeField] private GameObject skillTreeCanvas;
    //[SerializeField] private SkillPanel skillBarPanel;
    //[SerializeField] private SkillPanel skillTreePanel;
    //[SerializeField] private SkillPopup skillPopup;
    // Craft
    [SerializeField] private GameObject craftCanvas;
    [SerializeField] private GameObject equipmentCanvas;

    public void ToggleToolbar()
    {
        toolBarCanvas.SetActive(!toolBarCanvas.activeInHierarchy);
    }

    public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);

        toolBarPanel.Show();

        toolBarCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
        dragDropIcon.SetActive(false);

        ToolTipPanel.Singleton.Hide();
    }

    //public void ToggleSkillTab()
    //{
    //    skillTreeCanvas.SetActive(!skillTreeCanvas.activeInHierarchy);

    //    skillBarPanel.UIUpdate();
    //    skillTreePanel.UIUpdate();
    //    skillPopup.Clear();

    //    toolBarCanvas.SetActive(!skillTreeCanvas.activeInHierarchy);
    //}

    public void ToggleCraftTab()
    {
        bool craftTabWasOn = craftCanvas.activeInHierarchy;
        craftCanvas.SetActive(!craftTabWasOn);
        inventoryCanvas.SetActive(!craftTabWasOn);
        toolBarPanel.Show();
        toolBarCanvas.SetActive(craftTabWasOn);

        if(craftTabWasOn)
        {
            CraftingSystem.Singleton.ReturnUnusedItems();
        }
    }

    public void ToggleEquipmentTab()
    {
        bool equipmentTabWasOn = equipmentCanvas.activeInHierarchy;
        equipmentCanvas.SetActive(!equipmentTabWasOn);
        inventoryCanvas.SetActive(!equipmentTabWasOn);
        toolBarPanel.Show();
        toolBarCanvas.SetActive(equipmentTabWasOn);
    }
}
