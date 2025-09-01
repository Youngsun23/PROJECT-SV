using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerHouseDoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string connectedRoomName;
    [SerializeField] private Vector2 destinationPosition; // �� �������� ����� �ٲ���ϴµ�
    [SerializeField] private Collider2D confiner;

    public void Interact(PlayerCharacterController character)
    {
        // animator.SetTrigger("Interact");
        // doorClosed.SetActive(false);
        // doorOpen.SetActive(true);

        // �ִϸ��̼� ��� �� UI ���� - �ڷ�ƾ/�ִϸ����Ϳ��� UIShow ȣ��/?

        // �� ��ȯ ��� ��ġ�� �̵�?
        // SceneTransitionManager.Singleton.LoadLevel(connectedRoomName, destinationPosition);
        // ��ȯ�ϴ� ���� ����?

        PlayerCharacterController.Singleton.transform.position = destinationPosition;
        CameraSystem.Singleton.SetCameraConfiner(confiner);
    }
}
