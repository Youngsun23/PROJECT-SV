using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject toolBarCanvas;
    [SerializeField] ItemPanel toolBarPanel;
    [SerializeField] GameObject dragDropIcon;

    public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);

        toolBarPanel.Show();

        toolBarCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
        dragDropIcon.SetActive(false);
    }
}
