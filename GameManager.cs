using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offSet;
    public GameObject enemy;
    public GameObject enemies;
    public int enemyCount = 0;

    GameObject newEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawEnemies();
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
