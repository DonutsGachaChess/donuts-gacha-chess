using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PlayerTile : TileBase
{
    public UnitType unitType;
    public Sprite sprite;

    // 0 is you, 1 is enemy. Gonna change this later
    public int owner;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
    }
}

public enum UnitType
{
    Cavadeer,
    TurxedoMask,
    Squizard 
}