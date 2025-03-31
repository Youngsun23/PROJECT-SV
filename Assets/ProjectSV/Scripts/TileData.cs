using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tile Data")]
public class TileData : ScriptableObject
{
    public List<TileBase> Tiles => tiles;
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private bool plowable;
}
