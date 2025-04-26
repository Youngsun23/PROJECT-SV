using UnityEngine;

public class UITabController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject toolBarCanvas;
    [SerializeField] private ItemPanel toolBarPanel;
    [SerializeField] private GameObject dragDropIcon;
    [SerializeField] private GameObject skillTreeCanvas;
    [SerializeField] private SkillPanel skillBarPanel;
    [SerializeField] private SkillPanel skillTreePanel;
    [SerializeField] private SkillPopup skillPopup;

    public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);

        toolBarPanel.Show();

        toolBarCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
        dragDropIcon.SetActive(false);
    }

    public void ToggleSkillTab()
    {
        skillTreeCanvas.SetActive(!skillTreeCanvas.activeInHierarchy);

        skillBarPanel.UIUpdate();
        skillTreePanel.UIUpdate();
        skillPopup.Clear();

        toolBarCanvas.SetActive(!skillTreeCanvas.activeInHierarchy);
    }
}
