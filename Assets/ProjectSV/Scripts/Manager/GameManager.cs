using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }

    public GameObject Player => player;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemContainer origin_inventory;
    public ItemContainer Inventory { get; private set; }
    public ItemDragDropController ItemDragDropController { get; private set; }

    private void Awake()
    {
        Instance = this;
        ItemDragDropController = GetComponent<ItemDragDropController>();    
        Inventory = Instantiate(origin_inventory);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
