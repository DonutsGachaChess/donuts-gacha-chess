using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public int maxUnits = 8;

    public PlayerTile fuzzem;
    public PlayerTile fuzzemArcher;

    private Tilemap playerTiles;
    private Tilemap highlightTilemap;
    // Start is called before the first frame update
    void Start()
    {
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        highlightTilemap = GameObject.FindGameObjectWithTag("HighlightTilemap").GetComponent<Tilemap>();

        int unitsAdded = 0;
        for (int i = 1; i < 17; i++)
        {
            for (int j = 9; j >= 8; j++)
            {
                if (unitsAdded < maxUnits)
                {
                    int coinFlip = Random.Range(0, 2);
                    if (coinFlip == 1)
                    {
                        int fuzzemFlip = Random.Range(0, 2);
                        if (fuzzemFlip == 0)
                        {
                            PlayerTile fuzzemInstance = ScriptableObject.CreateInstance<PlayerTile>();
                            fuzzemInstance.sprite = fuzzem.sprite;
                            fuzzemInstance.owner = fuzzem.owner;
                            fuzzemInstance.unitType = fuzzem.unitType;
                            playerTiles.SetTile(new Vector3Int(i, j, 0), fuzzemInstance);
                        }
                        else
                        {
                            PlayerTile fuzzemInstance = ScriptableObject.CreateInstance<PlayerTile>();
                            fuzzemInstance.sprite = fuzzemArcher.sprite;
                            fuzzemInstance.owner = fuzzemArcher.owner;
                            fuzzemInstance.unitType = fuzzemArcher.unitType;
                            playerTiles.SetTile(new Vector3Int(i, j, 0), fuzzemInstance);
                        }
                        unitsAdded++;
                    }
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


        // Reset moved flag after moving every unit
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                PlayerTile currentTile = (PlayerTile)playerTiles.GetTile(new Vector3Int(i, j, 0));
                if (currentTile != null && currentTile.owner == 1)
                {
                    currentTile.hasMoved = false;
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
            default:
                newPosition = new Tuple<int, int>(currentX, currentY - 1);
                break;
        }

        PlayerTile nextTile = (PlayerTile) playerTiles.GetTile(new Vector3Int(newPosition.Item1, newPosition.Item2, 0));

        // Don't destroy enemy piece
        if ((nextTile == null || nextTile.owner == 0) && currentTile.hasMoved == false)
        {
            playerTiles.SetTile(new Vector3Int(newPosition.Item1, newPosition.Item2, 0), currentTile);
            playerTiles.SetTile(new Vector3Int(currentX, currentY, 0), null);
            currentTile.hasMoved = true;
        }
    }
}
