using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public PlayerTile fuzzem;
    public PlayerTile fuzzemArcher;

    private GameController gameController;

    private Tilemap playerTiles;
    private Tilemap highlightTilemap;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        highlightTilemap = GameObject.FindGameObjectWithTag("HighlightTilemap").GetComponent<Tilemap>();

        int unitsAdded = 0;
        int maxUnits = 8;
        for (int i = 1; i < 17; i++)
        {
            if (unitsAdded <= maxUnits)
            {
                int coinFlip = Random.Range(0, 2);
                if (coinFlip == 1)
                {
                    int fuzzemFlip = Random.Range(0, 2);
                    if (fuzzemFlip == 0)
                    {
                        playerTiles.SetTile(new Vector3Int(i, 9, 0), fuzzem);
                    }
                    else
                    {
                        playerTiles.SetTile(new Vector3Int(i, 9, 0), fuzzemArcher);
                    }
                    unitsAdded++;
                }
            }
        }
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
            case UnitType.Fuzzem:
                newPosition = new Tuple<int, int>(currentX, currentY - 1);
                break;
            case UnitType.FuzzemArcher:
                int newX = currentX + Random.Range(-1, 2);
                if (newX <= 1) newX = 1;
                if (newX >= 15) newX = 15;
                newPosition = new Tuple<int, int>(newX, currentY - 1);
                break;
        }

        PlayerTile nextTile = (PlayerTile) playerTiles.GetTile(new Vector3Int(newPosition.Item1, newPosition.Item2, 0));

        // Don't destroy enemy piece
        if (nextTile == null || nextTile.owner != 1)
        {
            playerTiles.SetTile(new Vector3Int(newPosition.Item1, newPosition.Item2, 0), currentTile);
            playerTiles.SetTile(new Vector3Int(currentX, currentY, 0), null);
        }

    }
}
