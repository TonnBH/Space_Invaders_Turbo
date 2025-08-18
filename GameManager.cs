using UnityEngine;
using TMPro;
using System;
using System.Collections;

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
    public int playerHealth = 3; // Player's health
    [HideInInspector] public bool playGame; // Flag to check if the game is over
    [SerializeField] GameObject[] playerLifes; // Reference to the player GameObject    

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

    [Header("Main Menu Settings")]
    public GameObject startGamePanel;
    const int freezeGame = 0; // Constant to pause the game
    const int unfreezeGame = 1; // Constant to unpause the game

    int level = 1; // Current level of the game

    [Header("End Game")]
    public GameObject endGamePanel; // Panel to show when the game ends
    public TMP_Text gameOverText;

    [Header("Exit game")]
    public GameObject exitGamePanel; // Panel to show when the game ends

    [Header("Game Manager Updates")]
    //public int hitPoints;
    public int timerResetCount;
    public float enemySpeed;
    public float enemyFireRate;
    public bool resetTimers;
    //public float levelEnemyFirerate = 158f; // Fire rate for enemies in the current level

    Vector3 EnemyRespawn = new Vector3(-1.6f, -4.5f, 0); // Respawn position for the player
    bool gameStart = true;
    bool resetEnemies;

    [Header("Reset Player")]
    public GameObject player; // Reference to the player GameObject that will be reset


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawEnemies();
        instance = this; // Singleton pattern to access GameManager from other scripts

        LevelDisplay(level);
        startGamePanel.SetActive(true); // Show the start game panel
        Time.timeScale = freezeGame; // Freeze the game at the start
    }

    // Update is called once per frame
    void Update()
    {
        LifeLostUpdate();
        ScoreSystem();
    }

    private void LifeLostUpdate()
    {
        if((lifeLost && playerHealth < 0) || gameOver)
        {
            playGame = false; // Set game state to not playing when player health is zero
            lifeLost = false; // Reset the life lost flag
            gameOver = false; // Reset the game over flag
            EndGame(); // Call the EndGame method to show the end game panel
        }

        else if (lifeLost)
        {
            playerHealth--; // Decrease player health when a life is lost
            playerLifes[playerHealth].SetActive(false); // Activate the player's life UI
            lifeLost = false; // Reset the life lost flag                   
        }

        if (enemyCount == 0 && playGame)
        {
            if (level == 3)
            {
                playGame = false; // Set game state to not playing when all enemies are defeated
                gameOverText.text = "You Win!"; // Update the game over text
                endGamePanel.SetActive(true); // Show the end game panel
                Time.timeScale = freezeGame; // Freeze the game         
            }
            else
            {
                playGame = false; // Set game state to not playing when all enemies are defeated
                LevelUp(); // Call the LevelUp method to advance to the next level              
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !startGamePanel.activeInHierarchy && !endGamePanel.activeInHierarchy)
        {
            if (exitGamePanel.activeInHierarchy)
            {
                NoExit(); // Hide the exit game panel
            }
            else
            {
                ExitPrompt(); // Show the exit game panel
            }
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

    void ResetEnemies()
    {
        enemyCount = 0; // Reset the enemy count
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                enemies.transform.GetChild(enemyCount).gameObject.SetActive(true); // Activactrd existing enemies
                enemies.transform.GetChild(enemyCount).position = new Vector3(i * offSet.x - 5.5f, j * offSet.y + 0.8f, 0);
                enemyCount++;
            }
        } 

        resetEnemies = false; // Reset the reset enemies flag
    }

    public void Restart()
    {
        if (startGamePanel.activeInHierarchy)
        {
            startGamePanel.SetActive(false); // Hide the start game panel
            Time.timeScale = unfreezeGame; // Unfreeze the game
        }

        if(endGamePanel.activeInHierarchy)
        {
            endGamePanel.SetActive(false); // Hide the end game panel
            Time.timeScale = unfreezeGame; // Unfreeze the game
        }

        level = 1; // Reset the level to 1
        LevelDisplay(level); // Update the level display
        score = 0; // Reset the score
        scoreText.text = score.ToString("00000"); // Update the score text
        playerHealth = 3; // Reset player health
        playerLifes[0].SetActive(true); // Activate the first life
        playerLifes[1].SetActive(true); // Activate the second life
        playerLifes[2].SetActive(true); // Activate the third life        
    }

    public void ResetGame()
    {
       StartCoroutine(GameReset()); // Start the GameReset coroutine
       StopAllCoroutines(); // Stop all running coroutines
    }
    IEnumerator GameReset()
    {
        GameObject[] objectsToDestroy;
        objectsToDestroy = GameObject.FindGameObjectsWithTag("Projectile"); // Find and destroy all projectiles in the scene

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj); // Destroy each projectile
        }

        for (int i = 0; i <= 4; i++)
        {
            playerLifes[i].SetActive(true); // Reset player lives
        }

        playerHealth = 3; // Reset player health 

        resetEnemies = true; // Set the reset enemies flag to true
        ResetEnemies(); // Reset the enemies

        while (resetEnemies)
        {
            yield return null; // Wait until enemies are reset
        }

        timerResetCount = 0; // Reset the timer reset count
        resetTimers = true; // Set the reset timers flag to true

        while (timerResetCount < enemyCount)
        {
            yield return null; // Wait until timers are reset
        }

        resetTimers = false; // Reset the reset timers flag        
    }

    void LevelUp(){
        level++; // Increment the level
        LevelDisplay(level); // Update the level display
        if (level == 2)
        {
            //enemyFireRate = levelEnemyFirerate / 2; // Adjust fire rate for level 2
            //enemySpeed = enemySpeed * 1.5f; // Increase enemy speed for level 2
            ResetGame(); // Reset the game for the new level
        }
        else if (level == 3)
        {
            //enemyFireRate = levelEnemyFirerate / 3; // Adjust fire rate for level 3
            //enemySpeed = enemySpeed * 2f; // Increase enemy speed for level 3
            ResetGame(); // Reset the game for the new level
        }
        // Reset enemies and timers if needed
        resetEnemies = true;
        resetTimers = true;
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

    void EndGame()
    {
        endGamePanel.SetActive(true); // Show the end game panel
        gameOverText.text = "Game Over!"; // Update the game over text
        Time.timeScale = freezeGame; // Freeze the game
        //playGame = false; // Set game state to not playing
    }

    public void ExitPrompt()
    {
        exitGamePanel.SetActive(true); // Show the exit game panel
        Time.timeScale = freezeGame; // Freeze the game
    }

    public void NoExit()
    {
        exitGamePanel.SetActive(false); // Hide the exit game panel
        Time.timeScale = unfreezeGame; // Unfreeze the game
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the application        
    }
}
