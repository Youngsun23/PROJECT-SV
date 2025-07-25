using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Crops Container")]
public class PlantedCropsContainer : ScriptableObject
{
    public List<CropTile> Crops => crops;
    [SerializeField] private List<CropTile> crops;

    public void SetCropTileList(List<CropTile> crops)
    {
        this.crops = crops;
    }

    public CropTile GetCropTile(Vector3Int pos)
    {
        return crops.Find(x => x.Position == pos);
    }

    public void ClearCropTileList()
    {
        crops.Clear();
    }
}
