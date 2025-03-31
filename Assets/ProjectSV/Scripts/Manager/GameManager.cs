using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }

    public GameObject player;
    [SerializeField] private ItemContainer origin_inventory;
    public ItemContainer inventory;
    public ItemDragDropController itemDragDropController;

    private void Awake()
    {
        Instance = this;
        inventory = Instantiate(origin_inventory);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
