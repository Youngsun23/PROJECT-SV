using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerCharacterToolController : MonoBehaviour
{
    private PlayerCharacterController character;
    private ToolBarController toolBarController;
    [SerializeField] private MarkerManager markerManager;
    [SerializeField] private TileMapReadManager tileMapReadManager;
    [SerializeField] private CropManager cropManager;
    private Rigidbody2D rigidBody;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float interactableRange = 1.2f;
    private Vector3Int selectedTilePos;
    private bool isTileSelectable;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private TileData plowableTile;

    private void Awake()
    {
        character = GetComponent<PlayerCharacterController>();
        toolBarController = GetComponent<ToolBarController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SelectTile();
        tileSelectableCheck();
        Marker();
    }

    private void SelectTile()
    {
        selectedTilePos = tileMapReadManager.GetGridPosition(Mouse.current.position.ReadValue());
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
        Vector3Int gridPos = selectedTilePos;
        markerManager.SetMarkedCellPosition(gridPos);
    }

    public bool UseToolWorld()
    {
        Vector2 position = rigidBody.position + character.lastMoveVector * offsetDistance;

        Item item = toolBarController.GetCurrentHoldingItem();
        if (item == null || item.OnToolAction == null)
            return false;

        bool isActionHit = item.OnToolAction.OnApply(position);
        return isActionHit;
    }

    public void UseToolGrid()
    {
        if(isTileSelectable)
        {
            TileBase tileBase = tileMapReadManager.GetTileBase(selectedTilePos);
            TileData tileData = tileMapReadManager.GetTileData(tileBase);
            if (tileData == null || tileData != plowableTile)
                return;

            if (cropManager.plowedCheck(selectedTilePos))
            {
                cropManager.Plant(selectedTilePos);
            }
            else
            {
                cropManager.Plow(selectedTilePos);
            }
        }
    }
}
