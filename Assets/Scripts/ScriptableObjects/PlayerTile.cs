using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PlayerTile : TileBase
{
    public UnitType unitType;
    public Sprite sprite;

    public bool hasMoved = false;

    // 0 is you, 1 is enemy. Gonna change this later
    public int owner;

    public PlayerTile(PlayerTile playerTile)
    {
        this.unitType = playerTile.unitType;
        this.sprite = playerTile.sprite;
        this.owner = playerTile.owner;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
    }
}

public enum UnitType
{
    Cavadeer,
    TurxedoMask,
    Squizard,
    Turtank,
    Snek,
    GrizzledGrizzly,
    Fuzzem,
    FuzzemArcher,
    Kirei
}