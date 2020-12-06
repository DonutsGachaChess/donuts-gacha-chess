using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject victoryUI;

    public GameObject gameOverText;
    public GameObject gameOverUI;

    public Text turnText;

    // 0 is player, 1 is opponent
    internal int turnOwner;
    internal bool isGameOver = false;

    private EnemyController enemyController;
    private Tilemap playerTiles;

    // Start is called before the first frame update
    void Start()
    {
        turnOwner = 0;
        enemyController = GetComponent<EnemyController>();
        playerTiles = GameObject.FindGameObjectWithTag("PlayerTiles").GetComponent<Tilemap>();

    }

    internal void EndTurn()
    {
        turnOwner = 1;
        turnText.text = "Enemy Phase";
        turnText.color = Color.red;
        enemyController.ProcessEnemyUnits();

        CheckGameOver();

        turnOwner = 0;
        turnText.text = "Player Phase";
        turnText.color = Color.blue;
    }

    private void CheckGameOver()
    {
        for (int i = 0; i < 20; i++)
        {
            PlayerTile currentTile = (PlayerTile)playerTiles.GetTile(new Vector3Int(i, 0, 0));
            if (currentTile != null && currentTile.owner == 1)
            {
                gameOverText.GetComponent<Text>().enabled = true;
                gameOverUI.SetActive(true);
                isGameOver = true;
                // is it possible to win and lose at the same time?
            }
        }

        bool enemyUnitExists = false;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                PlayerTile currentTile = (PlayerTile)playerTiles.GetTile(new Vector3Int(i, j, 0));
                if (currentTile != null && currentTile.owner == 1)
                {
                    enemyUnitExists = true;
                }
            }
        }
        if (!enemyUnitExists)
        {
            victoryUI.SetActive(true);
            isGameOver = true;
        }

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadSurprise()
    {
        SceneManager.LoadScene("Surprise");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
