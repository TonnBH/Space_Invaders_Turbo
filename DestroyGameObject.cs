using UnityEngine;


public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.1f;

    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime); // Destroy the game object after a certain time to prevent memory leaks
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
