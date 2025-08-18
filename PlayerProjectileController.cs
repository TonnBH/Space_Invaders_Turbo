using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [Header("Projectile Speed")]
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] float lifeTime = 1.2f;

    [SerializeField] GameObject enemyDestroyed; // Reference to the bullet prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,lifeTime);// Destroy the projectile after a certain time to prevent memory leaks
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, projectileSpeed * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
        SoundEffectManager.Play("EnemyHit"); // Play enemy hit sound effect        
        Destroy(gameObject);
        GameManager.instance.playGame = true; // Set game state to playing
        GameManager.instance.enemyCount--; // Decrease enemy count when projectile hits an enemy
        Instantiate(enemyDestroyed, collision.transform.position, Quaternion.identity); // Instantiate enemy destroyed effect
        GameManager.instance.score += 10; // Increase score by 10 when an enemy is hit
    }
}