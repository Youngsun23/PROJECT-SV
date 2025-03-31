using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerCharacterToolController : MonoBehaviour
{
    private PlayerCharacterController character;
    [SerializeField] private MarkerManager markerManager;
    [SerializeField] private TileMapReadManager tileMapReadManager;
    [SerializeField] private CropManager cropManager;
    private Rigidbody2D rigidBody;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float interactableRange = 1.2f;
    private Vector3Int selectedTilePos;
    private bool selectable;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private TileData plowableTile;

    private void Awake()
    {
        character = GetComponent<PlayerCharacterController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SelectTile();
        tileSelectableCheck();
        Marker();
    }

    private void Marker()
    {
        Vector3Int gridPos = selectedTilePos;
        markerManager.SetMarkedCellPosition(gridPos);
    }

    private void SelectTile()
    {
        selectedTilePos = tileMapReadManager.GetGridPosition(Mouse.current.position.ReadValue());
    }

    private void tileSelectableCheck()
    {
        Vector2 characterPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        selectable = Vector2.Distance(characterPos, cameraPos) < maxDistance;
        markerManager.ToggleMarker(selectable);
    }

    public bool UseToolWorld()
    {
        Vector2 position = rigidBody.position + character.lastMoveVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactableRange);
        foreach(Collider2D col in colliders)
        {
            ToolHit hit = col.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                return true;
            }
        }
        return false;
    }

    public void UseToolGrid()
    {
        if(selectable)
        {
            TileBase tileBase = tileMapReadManager.GetTileBase(selectedTilePos);
            TileData tileData = tileMapReadManager.GetTileData(tileBase);
            if (tileData == null || tileData != plowableTile)
                return;

            if (cropManager.Check(selectedTilePos))
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
