using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceSelector : MonoBehaviour
{
    public TileBase highlightedTile;

    private GameController gameController;

    private Tilemap playerTiles;
    private Tilemap highlightTilemap;

    private bool pieceSelected = false;
    private PlayerTile currentlySelectedTile;
    private int selectedTileXCoord;
    private int selectedTileYCoord;

    private List<Tuple<int, int>> possibleMoveCoords = new List<Tuple<int, int>>();

    private List<Tuple<int, int>> highlightedTileCoords = new List<Tuple<int, int>>();

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        highlightTilemap = GameObject.FindGameObjectWithTag("HighlightTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.turnOwner == 0 && !gameController.isGameOver)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // Left mouse button
            if (Input.GetMouseButtonUp(0))
            {
                HandleLeftMouseButton(hit);
            }

            // Right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                RemoveHighlights();

                currentlySelectedTile = null;
                pieceSelected = false;
            }
        } 
        else
        {
            // Display some message that it's not the player's turn?
        }
    }

    private void HandleLeftMouseButton(RaycastHit2D hit)
    {
        int xCoord = Mathf.FloorToInt(hit.point.x);
        int yCoord = Mathf.FloorToInt(hit.point.y);

        PlayerTile clickedTile = (PlayerTile)playerTiles.GetTile(new Vector3Int(xCoord, yCoord, 0));
        if (!pieceSelected)
        {
            // Can only select your pieces
            if (clickedTile != null && clickedTile.owner == 0)
            {
                highlightedTileCoords.Add(new Tuple<int, int>(xCoord, yCoord));
                possibleMoveCoords = GetPossibleMoves(clickedTile, xCoord, yCoord);
                highlightedTileCoords.AddRange(possibleMoveCoords);
                foreach (Tuple<int, int> coords in highlightedTileCoords)
                {
                    highlightTilemap.SetTile(new Vector3Int(coords.Item1, coords.Item2, 0), highlightedTile);
                }

                currentlySelectedTile = clickedTile;

                pieceSelected = true;
                selectedTileXCoord = xCoord;
                selectedTileYCoord = yCoord;
            }
        }
        else
        {
            Tuple<int, int> clickedTileCoord = new Tuple<int, int>(xCoord, yCoord);
            bool validMove = possibleMoveCoords.Contains(clickedTileCoord);

            if ((clickedTile == null || clickedTile.owner != currentlySelectedTile.owner) && validMove)
            {
                pieceSelected = false;
                playerTiles.SetTile(new Vector3Int(xCoord, yCoord, 0), currentlySelectedTile);

                // Remove tile from old position
                playerTiles.SetTile(new Vector3Int(selectedTileXCoord, selectedTileYCoord, 0), null);

                RemoveHighlights();
                gameController.EndTurn();
            }
            else if (clickedTile != null && clickedTile.owner == currentlySelectedTile.owner)
            {
                Debug.LogError("tried to take own piece");
                // TODO: display error to user, sound effect
                currentlySelectedTile = null;
                pieceSelected = false;
                RemoveHighlights();
            }
        }
    }

    private List<Tuple<int, int>> GetPossibleMoves(PlayerTile clickedTile, int xCoord, int yCoord)
    {
        List<Tuple<int, int>> movementCoords = new List<Tuple<int, int>>();

        switch (clickedTile.unitType)
        {
            case UnitType.Cavadeer:
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 3, yCoord + 3));

                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord - 3, yCoord - 3));

                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 3, yCoord - 3));

                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord - 3, yCoord + 3));
                break;
            case UnitType.TurxedoMask:
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 1));
                break;
            case UnitType.Squizard:
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 2));
                // TODO: add ranged capture
                break;
            case UnitType.Snek:
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 3));

                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 3));
                break;
            case UnitType.Turtank:
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));

                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 2));

                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 3));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 3));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 3));

                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 3));

                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 3, yCoord));

                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 3, yCoord));
                break;
            case UnitType.GrizzledGrizzly:
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 2));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 3));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 4));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 5));

                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 2));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 3));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 4));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 5));

                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 2, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 3, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 4, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 5, yCoord));

                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 2, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 3, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 4, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord - 5, yCoord));

                break;
            case UnitType.Kirei:
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        movementCoords.Add(new Tuple<int, int>(i, j));
                    }
                }
                break;
            default:
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord - 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord - 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord + 1, yCoord));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord + 1));
                movementCoords.Add(new Tuple<int, int>(xCoord, yCoord - 1));
                break;
        }

        return movementCoords;

    }

    private void RemoveHighlights()
    {
        foreach (Tuple<int, int> coord in highlightedTileCoords)
        {
            highlightTilemap.SetTile(new Vector3Int(coord.Item1, coord.Item2, 0), null);
        }
        highlightedTileCoords.Clear();
    }
}
