using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceSelector : MonoBehaviour
{
    public TileBase emptyTile;

    private Tilemap playerTiles;

    private bool pieceSelected = false;
    private TileBase selectedTile;
    private int selectedTileXCoord;
    private int selectedTileYCoord;

    // Start is called before the first frame update
    void Start()
    {
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            Vector2 point = hit.point;
        }

        
        
        if (Input.GetMouseButtonUp(0))
        {
            int xCoord = Mathf.FloorToInt(hit.point.x);
            int yCoord = Mathf.FloorToInt(hit.point.y);
            if (!pieceSelected)
            {

                selectedTile = playerTiles.GetTile(new Vector3Int(xCoord, yCoord, 0));

                pieceSelected = true;
                selectedTileXCoord = xCoord;
                selectedTileYCoord = yCoord;
            }
            else
            {
                // TODO: require valid moves
                playerTiles.SetTile(new Vector3Int(xCoord, yCoord, 0), selectedTile);

                pieceSelected = false;

                // Remove tile from old position
                playerTiles.SetTile(new Vector3Int(selectedTileXCoord, selectedTileYCoord, 0), emptyTile);
            }

        }
    }
}
