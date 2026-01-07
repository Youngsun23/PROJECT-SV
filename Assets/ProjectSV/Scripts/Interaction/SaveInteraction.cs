using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteraction : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacterController character)
    {
        // Debug.Log("UserData�� �����մϴ�.");

        // �� �� ����? ����� UI ����, OK ������ �ڷ�ƾ ȣ��

        StartCoroutine(BedSave());
    }

    IEnumerator BedSave()
    {
        // ħ�뿡 �� ���ڴ� �ִϸ��̼� Ʋ��, 
        // ��ٷȴٰ�,

        PlayerCharacter.Singleton.DisableCharacterControl.DisableControl();

        var FadeUI = UIManager.Singleton.GetUI<FadeInOutUI>(UIType.FadeInOut);

        FadeUI.FadeOut();
        yield return new WaitForSeconds(1);

        CropManager.Singleton.TileMapCropManger.SaveCropTilesData();
        PlaceableObjectsManager.Singleton.SavePlacedItemsData();
        PlayerCharacter.Singleton.CharacterResourceComponent.SaveCharacterResourceData();
        UserDataManager.Singleton.Save();

        PlayerCharacter.Singleton.WakeUp();
        TimeManager.Singleton.StartNextDay();

        FadeUI.FadeIn();
        yield return new WaitForSeconds(1);

        PlayerCharacter.Singleton.DisableCharacterControl.EnableControl();
    }
}
