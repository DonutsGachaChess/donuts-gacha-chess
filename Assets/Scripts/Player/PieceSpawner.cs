using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceSpawner : MonoBehaviour
{
    public TileBase[] tiles;
    public GameObject[] test;

    public TileBase testTile;

    private Tilemap playerTiles;
    // Start is called before the first frame update
    void Start()
    {
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        playerTiles.SetTile(new Vector3Int(0, 0, 0), testTile);
        for (int i = 0; i < 20; i++)
        {
            playerTiles.SetTile(new Vector3Int(i, 0, 0), testTile);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
