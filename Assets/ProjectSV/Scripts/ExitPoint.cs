using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string destinationScene;
    [SerializeField] private Vector2 destinationPosition;

    //private void Awake()
    //{
    //    SceneTransitionManager.Singleton.AddTransitionPoint(this);
    //}

    public void Interact(PlayerCharacterController character)
    {
        SceneTransitionManager.Singleton.LoadLevel(destinationScene, destinationPosition);
    }
}