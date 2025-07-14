using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Movement")]
    float timer = 0;
    float movementAmount = 0.1f;
    int numOfMovement = 0;

    [Header("Enemy Fire")]
    [SerializeField] GameObject bulletPrefab;
    GameObject bulletPrefabClone;

    float fireRate = 0;
    float timeOfFire = 0.1f;
    float maxY = -3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playGame == true)
        {
            timer += Time.deltaTime;
            MoveHorizontally();
            MoveVertically();

            fireRate += Time.deltaTime;

            if (fireRate > timeOfFire)
            {
                FireEnemyProjectile();
                fireRate = 0;
            }
        }
    }

    void MoveHorizontally()
    {
        if(timer > 0.5f && numOfMovement != 17)
        {
           transform.Translate(new Vector3(movementAmount, 0, 0));
           timer = 0;
           numOfMovement++;
        }
    }

    void MoveVertically() 
    {
        if (numOfMovement == 17)
        {
            transform.Translate(new Vector3(0, -0.1f, 0));
            numOfMovement = 0;
            timer = 0;
            movementAmount *= -1; // Reverse direction

            if (transform.position.y < maxY)
            {
                GameManager.instance.gameOver = true; // Stop the game if enemies reach the bottom
                GameManager.instance.playGame = false; // Set game state to not playing
                Debug.Log("Enemies reached the bottom! Game Over.");
            }
        }
    }

    void FireEnemyProjectile()
    {
        if (Random.Range(0f, 125f) < 1) 
        {
            bulletPrefabClone = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 0.6f), transform.rotation);
        }
    }
}
