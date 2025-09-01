using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerHouseDoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string connectedRoomName;
    [SerializeField] private Vector2 destinationPosition; // 이 원시적인 방법을 바꿔야하는데
    [SerializeField] private Collider2D confiner;

    public void Interact(PlayerCharacterController character)
    {
        // animator.SetTrigger("Interact");
        // doorClosed.SetActive(false);
        // doorOpen.SetActive(true);

        // 애니메이션 출력 후 UI 띄우기 - 코루틴/애니메이터에서 UIShow 호출/?

        // 씬 전환 대신 위치만 이동?
        // SceneTransitionManager.Singleton.LoadLevel(connectedRoomName, destinationPosition);
        // 전환하는 듯한 연출?

        PlayerCharacterController.Singleton.transform.position = destinationPosition;
        CameraSystem.Singleton.SetCameraConfiner(confiner);
    }
}
