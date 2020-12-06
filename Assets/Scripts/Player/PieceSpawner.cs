using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceSpawner : MonoBehaviour
{
    public int maxUnits = 5;
    public TileBase testTile;
    public PlayerTile[] playerUnits;

    private Tilemap playerTiles;
    // Start is called before the first frame update
    void Start()
    {
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();
        //playerTiles.SetTile(new Vector3Int(0, 0, 0), testTile);
        //playerTiles.SetTile(new Vector3Int(17, 0, 0), testTile);

        int currentUnits = 0;
        for (int i = 1; i <= 16; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                int roll = Random.Range(1, 101);
                if (roll <= 13 && currentUnits < maxUnits)
                {
                    PlayerTile tile = ScriptableObject.CreateInstance<PlayerTile>();
                    int typeRoll = Random.Range(0, 5);

                    PlayerTile template = playerUnits[typeRoll];
                    tile.sprite = template.sprite;
                    tile.owner = template.owner;
                    tile.unitType = template.unitType;

                    playerTiles.SetTile(new Vector3Int(i, j, 0), tile);
                    currentUnits++;
                }
            }
        }
    }
}
