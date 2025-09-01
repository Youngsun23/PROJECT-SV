using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteraction : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacterController character)
    {
        Debug.Log("UserData를 저장합니다.");

        CropManager.Singleton.TileMapCropManger.SaveCropTilesData();
        PlaceableObjectsManager.Singleton.SavePlacedItemsData();
        UserDataManager.Singleton.Save();
    }
}
