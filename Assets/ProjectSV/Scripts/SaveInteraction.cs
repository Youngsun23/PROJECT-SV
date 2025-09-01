using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteraction : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacterController character)
    {
        Debug.Log("UserData�� �����մϴ�.");

        CropManager.Singleton.TileMapCropManger.SaveCropTilesData();
        PlaceableObjectsManager.Singleton.SavePlacedItemsData();
        UserDataManager.Singleton.Save();
    }
}
