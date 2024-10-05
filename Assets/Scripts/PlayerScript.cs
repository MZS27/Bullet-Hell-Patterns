using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float movementSpeed = 5f;  // Player movement speed
    public float bulletSpeed = 10f;   // Bullet speed
    public GameObject bulletPrefab;   // Bullet prefab
    public Transform bulletSpawnPoint; // The point where bullets are spawned

    private Rigidbody2D rb;

    private Vector2 movementInput;
    private Vector2 aimInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle movement input from keyboard or left joystick
        movementInput.x = Input.GetAxisRaw("LeftStickHorizontal"); // Left joystick X or keyboard A/D or Left/Right arrow
        movementInput.y = Input.GetAxisRaw("LeftStickVertical");   // Left joystick Y or keyboard W/S or Up/Down arrow
        
        // Normalize the direction to avoid faster diagonal movement
        movementInput = movementInput.normalized;
        
        aimInput = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));


        // Check for shooting input
        if (aimInput != Vector2.zero)
        {
            Shoot(aimInput);
        }
    }

    private void FixedUpdate()
    {
        // Move the player using the movement input vector
        if(movementInput != Vector2.zero)
        {
            rb.velocity = movementInput * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Shoot(Vector2 aimDirection)
    {
        // Create the bullet and shoot it in the aiming direction
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Normalize aim direction to ensure bullets shoot correctly in all 360 degrees
        aimDirection = aimDirection.normalized;

        // Set bullet velocity
        bulletRb.velocity = aimDirection * bulletSpeed;

        // Rotate player or weapon to face the aim direction
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
