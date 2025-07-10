using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [Header("Projectile Speed")]
    [SerializeField] float projectileSpeed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, projectileSpeed * Time.deltaTime, 0));
    }
}
