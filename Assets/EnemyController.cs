using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private GameController gameController;

    private Tilemap playerTiles;
    private Tilemap highlightTilemap;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        highlightTilemap = GameObject.FindGameObjectWithTag("HighlightTilemap").GetComponent<Tilemap>();
    }

    public void ProcessEnemyUnits()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                PlayerTile currentTile = (PlayerTile) playerTiles.GetTile(new Vector3Int(i, j, 0));
                if (currentTile != null && currentTile.owner == 1)
                {
                    MovePiece(currentTile, i, j);
                }
            }
        }
    }

    private void MovePiece(PlayerTile currentTile, int currentX, int currentY)
    {
        Tuple<int, int> newPosition = new Tuple<int, int>(0, 0);
        // TODO: next enemy highlights
        switch (currentTile.unitType)
        {
            case UnitType.TurxedoMask:
                newPosition = new Tuple<int, int>(currentX, currentY - 1);
                break;
        }

        playerTiles.SetTile(new Vector3Int(newPosition.Item1, newPosition.Item2, 0), currentTile);
        playerTiles.SetTile(new Vector3Int(currentX, currentY, 0), null);
    }
}
