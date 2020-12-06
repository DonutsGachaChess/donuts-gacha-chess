using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PlayerTile : TileBase
{
    [SerializeField, SerializeReference]
    public UnitType unitType;
    public Sprite sprite;

    // 0 is you, 1 is enemy. Gonna change this later
    public int owner;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
    }


}


public abstract class UnitType : MonoBehaviour
{
    public abstract List<Tuple<int, int>> getPossibleMoves(int xCoord, int yCoord);
}

public class Cavadeer : UnitType
{
    public override List<Tuple<int, int>> getPossibleMoves(int xCoord, int yCoord)
    {
        List<Tuple<int, int>> movementCoords = new List<Tuple<int, int>>();

        movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));
        movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord + 2));

        movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 1));
        movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord - 2));

        movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord - 1));
        movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord - 2));

        movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 1));
        movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord + 2));

        return movementCoords;
    }

}