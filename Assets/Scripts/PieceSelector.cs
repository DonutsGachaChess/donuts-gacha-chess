using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceSelector : MonoBehaviour
{
    public TileBase emptyTile;
    public TileBase highlightedTile;

    private Tilemap playerTiles;
    private Tilemap highlightTilemap;

    private bool pieceSelected = false;
    private PlayerTile currentlySelectedTile;
    private int selectedTileXCoord;
    private int selectedTileYCoord;

    private List<Tile> highlightedTiles;

    // Start is called before the first frame update
    void Start()
    {
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        highlightTilemap = GameObject.FindGameObjectWithTag("HighlightTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            Vector2 point = hit.point;
        }

        
        // Left mouse button
        if (Input.GetMouseButtonUp(0))
        {
            int xCoord = Mathf.FloorToInt(hit.point.x);
            int yCoord = Mathf.FloorToInt(hit.point.y);

            PlayerTile clickedTile = (PlayerTile) playerTiles.GetTile(new Vector3Int(xCoord, yCoord, 0));
            if (!pieceSelected)
            {
                highlightTilemap.SetTile(new Vector3Int(xCoord, yCoord, 0), highlightedTile);

                // TODO: add movement highlight

                currentlySelectedTile = clickedTile;

                pieceSelected = true;
                selectedTileXCoord = xCoord;
                selectedTileYCoord = yCoord;
            }
            else
            {
                // TODO: require valid moves

                if (clickedTile == null || clickedTile.owner != currentlySelectedTile.owner)
                {
                    pieceSelected = false;
                    playerTiles.SetTile(new Vector3Int(xCoord, yCoord, 0), currentlySelectedTile);

                    // Remove tile from old position
                    playerTiles.SetTile(new Vector3Int(selectedTileXCoord, selectedTileYCoord, 0), emptyTile);
                }
                else
                {
                    Debug.LogError("tried to take own piece");
                    // TODO: display error to user, sound effect
                    currentlySelectedTile = null;
                    pieceSelected = false;
                }

            }

        }

        // Right mouse button
        if (Input.GetMouseButtonUp(1))
        {
            // TODO: remove highlight
            currentlySelectedTile = null;
            pieceSelected = false;
        }
    }
}
