using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerCharacterToolController : MonoBehaviour
{
    private PlayerCharacterController character;
    private Animator animator;
    private ToolBarController toolBarController;
    [SerializeField] private MarkerManager markerManager;
    private Rigidbody2D rigidBody;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float interactableRange = 1.2f;
    private Vector3Int selectedTilePos;
    private bool isTileSelectable;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private ToolAction onTilePickup;

    private void Awake()
    {
        character = GetComponent<PlayerCharacterController>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        toolBarController = UIManager.Singleton.GetUI<ToolBarUI>(UIType.ToolBar).GetComponent<ToolBarController>();
    }

    private void Update()
    {
        if (TileMapReadManager.Singleton.TargetMap == null) return;
        if (markerManager == null) return;

        SelectTile();
        tileSelectableCheck();
        Marker();
    }

    private void SelectTile()
    {
        selectedTilePos = TileMapReadManager.Singleton.GetGridPosition(Mouse.current.position.ReadValue());
    }

    public void SetMarkerManager(MarkerManager target)
    {
        markerManager = target;
    }

    private void tileSelectableCheck()
    {
        Vector2 characterPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        isTileSelectable = Vector2.Distance(characterPos, cameraPos) < maxDistance;
        markerManager.ToggleMarker(isTileSelectable);
    }

    private void Marker()
    {
        // Vector3Int gridPos = selectedTilePos;
        markerManager.SetMarkedCellPosition(selectedTilePos);
        GameManager.Singleton.PlaceableItemHighlight.SetTargetCellPosition(selectedTilePos);
    }

    public bool UseToolWorld()
    {
        Vector2 position = rigidBody.position + character.lastMoveVector * offsetDistance;

        Item item = toolBarController.GetCurrentHoldingItem();
        if (item == null || item.OnToolAction == null)
            return false;

        // animator.SetTrigger("Act");
        bool actionComplete = item.OnToolAction.OnApply(position);
        return actionComplete;
    }

    public void UseToolGrid()
    {

        if (isTileSelectable)
        {
            Item item = toolBarController.GetCurrentHoldingItem();
            if(item == null)
            {
                PickupTile();
                return;
            }
            if (item.OnToolActionTileMap == null)
                return;

            TileBase tileBase = TileMapReadManager.Singleton.GetTileBase(selectedTilePos);
            TileData tileData = TileMapReadManager.Singleton.GetTileData(tileBase);
            if(tileData == null)
                return;

            //Debug.Log("ToolAction type: " + item.OnToolActionTileMap.GetType());

            // animator.SetTrigger("Act");
            bool actionComplete = item.OnToolActionTileMap.OnApplyTileMap(selectedTilePos, tileData, item);
            if(actionComplete)
            {
                item.OnToolActionTileMap.OnItemUsed(item, GameManager.Singleton.Inventory);
            }
        }
    }

    private void PickupTile()
    {
        if (onTilePickup == null) return;

        TileBase tileBase = TileMapReadManager.Singleton.GetTileBase(selectedTilePos);
        TileData tileData = TileMapReadManager.Singleton.GetTileData(tileBase);

        onTilePickup.OnApplyTileMap(selectedTilePos, tileData, null);
    }
}
