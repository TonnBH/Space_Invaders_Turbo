using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] GameObject player;
    [SerializeField] float speed = 5f;
    float horizontalInput;

    [Header("Player Movin OFF")]
    [SerializeField] float maxX;

    [Header("Input Fire")]
    [SerializeField] GameObject bulletPrefab;
    GameObject bulletPrefabClone;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
    }

    private void InputSystem()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if ((horizontalInput > 0 && player.transform.position.x < maxX) ||
            (horizontalInput < 0 && player.transform.position.x > -maxX))
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Space) && bulletPrefabClone == null)
        {
            Fire(); 
            SoundEffectManager.Play("PlayerFire"); // Play Player fire sound effect\            
        }
    }

    void Movement()
    {
        transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;        
    }

    private void Fire()
    {
        bulletPrefabClone = Instantiate(bulletPrefab, new Vector3(player.transform.position.x,player.transform.position.y + 0.6f), player.transform.rotation);
    }
}
