using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemy Settings")]
    public Vector2Int size;
    public Vector2 offSet;
    public GameObject enemy;
    public GameObject enemies;
    public int enemyCount = 0;

    [Header("Player Health")]
    public bool lifeLost = false; // Flag to check if a life is lost
    [HideInInspector] public bool playGame; // Flag to check if the game is over
    [SerializeField] GameObject[] playerLifes; // Reference to the player GameObject
    [SerializeField] int playerHealth = 3; // Player's health
    [HideInInspector] public Vector3 respawn = new Vector3(-1.6f, -4.5f, 0); // Respawn position for the player

    GameObject newEnemy;
    public bool gameOver = false; // Flag to check if the game is over

    [Header("Score Manager")]
    public TMP_Text scoreText;
    public int score = 0;

    [Header("Level Settings")]
    public GameObject Level_2On;
    public GameObject Level_2Off;
    public GameObject Level_3On;
    public GameObject Level_3Off;
    public TMP_Text levelText;

    int level = 1; // Current level of the game

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawEnemies();
        instance = this; // Singleton pattern to access GameManager from other scripts

        LevelDisplay(level);
    }

    // Update is called once per frame
    void Update()
    {
        LifeLostUpdate();
        ScoreSystem();
    }

    private void LifeLostUpdate()
    {
        if((lifeLost && playerHealth <= 0) || gameOver)
        {
            playGame = false; // Set game state to not playing when player health is zero
            lifeLost = false; // Reset the life lost flag
            gameOver = false; // Reset the game over flag
            Debug.Log("Game Over!"); // Log game over message
        }

        else if (lifeLost)
        {
            playerHealth--; // Decrease player health when a life is lost
            playerLifes[playerHealth].SetActive(false); // Activate the player's life UI
            lifeLost = false; // Reset the life lost flag                   
        }

        if (enemyCount <= 0 && playGame)
        {
            playGame = false; // Set game state to not playing when all enemies are defeated
            Debug.Log("All enemies defeated! You win!"); // Log win message
        }
    }
    void DrawEnemies()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                newEnemy = Instantiate(enemy, enemies.transform);
                newEnemy.transform.position = new Vector3(i * offSet.x - 5.5f, j * offSet.y + 0.8f, 0); 
                enemyCount++;
            }
        }
    }

    void ScoreSystem() 
    {
        scoreText.text = score.ToString("00000");
    }

    void LevelDisplay(int newLevel)
    {
        switch (newLevel)
        {
            case 1:
                Level_2On.SetActive(false);
                Level_2Off.SetActive(true);
                Level_3On.SetActive(false);
                Level_3Off.SetActive(true);
                //levelText.text = "Level 1";
                break;

            case 2:
                Level_2On.SetActive(true);
                Level_2Off.SetActive(false);
                Level_3On.SetActive(false);
                Level_3Off.SetActive(true);
                //levelText.text = "Level 2";
                break;

            case 3:
                Level_2On.SetActive(false);
                Level_2Off.SetActive(true);
                Level_3On.SetActive(true);
                Level_3Off.SetActive(false);
                //levelText.text = "Level 3";
                break;
        }

        levelText.text = newLevel.ToString("0"); // Update the level text
    }
}
