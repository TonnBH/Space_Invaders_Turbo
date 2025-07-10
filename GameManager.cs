using UnityEngine;

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
    bool lifeLost = false; // Flag to check if a life is lost
    bool playGame = false; // Flag to check if the game is over
    [SerializeField] int playerHealth = 3; // Player's health
    [SerializeField] GameObject[] playerLifes; // Reference to the player GameObject

    GameObject newEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawEnemies();
        instance = this; // Singleton pattern to access GameManager from other scripts
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
