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
        //Show(false); // �̰� ����
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
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>(); // �̰� ���ϴ� �ذ�-> ���� ����...��� �𸣰ڳ�...

        spriteRenderer.sprite = icon;
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}

// �Ÿ� ���� - ���̶���Ʈ ���̴� ��, Ŭ�� ��ġ �� ������� �ʰ�
// ���̶���Ʈ ���� ����, ���� ���� ��Ŀ ����, ��Ŀ2(������ ũ�� �ڵ� ����) �Ѽ�, �Ÿ� ���� �����ֱ�
