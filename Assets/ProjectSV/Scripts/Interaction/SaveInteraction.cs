using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteraction : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacterController character)
    {
        // Debug.Log("UserData를 저장합니다.");

        // 잠 잘 거임? 물어보는 UI 띄우고, OK 누르면 코루틴 호출

        StartCoroutine(BedSave());
    }

    IEnumerator BedSave()
    {
        // 침대에 들어가 잠자는 애니메이션 틀고, 
        // 기다렸다가,

        PlayerCharacter.Singleton.DisableCharacterControl.DisableControl();

        var FadeUI = UIManager.Singleton.GetUI<FadeInOutUI>(UIType.FadeInOut);

        FadeUI.FadeOut();
        yield return new WaitForSeconds(1);

        CropManager.Singleton.TileMapCropManger.SaveCropTilesData();
        PlaceableObjectsManager.Singleton.SavePlacedItemsData();
        UserDataManager.Singleton.Save();

        PlayerCharacter.Singleton.WakeUp();
        TimeManager.Singleton.StartNextDay();

        FadeUI.FadeIn();
        yield return new WaitForSeconds(1);

        PlayerCharacter.Singleton.DisableCharacterControl.EnableControl();
    }
}
