using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Tool Action/Pickup")]
public class PickupAction : ToolAction
{
    public override bool OnApplyTileMap(Vector3Int pos, TileData tile, Item usedItem)
    {
        // 농사 수확 픽업으로 한정
        // 채집 액션도 똑같은 걸로 할건지 함수 분리할건지?
        CropManager.Singleton.Pickup(pos);

        return true;
    }
}
