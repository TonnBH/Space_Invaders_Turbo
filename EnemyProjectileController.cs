using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float lifeTime = 1.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after a certain time to prevent memory leaks
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -bulletSpeed * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false); // Deactivate player
            Destroy(gameObject); // Destroy the projectile
        }
    }
}

