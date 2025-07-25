using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class PlaceableItemHighlight : MonoBehaviour
{
    private Vector3Int targetCellPosition;
    private Vector3 targetWorldPosition;
    [SerializeField] private Tilemap targetTileMap;
    private SpriteRenderer spriteRenderer;
    private bool isTileSelectable;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Show(false); // 이거 빼고
    }

    private void Update()
    {
        if (targetTileMap == null) return;

        targetWorldPosition = targetTileMap.CellToWorld(targetCellPosition);
        transform.position = targetWorldPosition + targetTileMap.cellSize/2;        
    }

    public void SetTargetCellPosition(Vector3Int pos)
    {
        this.targetCellPosition = pos;
    }

    public void SetTargetTileMap(Tilemap target)
    {
        targetTileMap = target;
    }

    public void SetIcon(Sprite icon)
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>(); // 이거 더하니 해결-> 순서 문제...계속 모르겠네...

        spriteRenderer.sprite = icon;
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}

// 거리 제한 - 하이라이트 보이는 것, 클릭 위치 맵 벗어나지만 않게
// 하이라이트 켰을 때는, 보통 때의 마커 끄고, 마커2(아이템 크기 자동 맞춤) 켜서, 거리 무관 보여주기
