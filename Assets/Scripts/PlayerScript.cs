using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public float movementSpeed = 5f;  // Player movement speed
    public float bulletSpeed = 10f;   // Bullet speed
    public GameObject bulletPrefab;   // Bullet prefab
    public Transform bulletSpawnPoint; // The point where bullets are spawned

    private Rigidbody2D rb;

    private Vector2 movementInput;
    private Vector2 aimInput;

    private NewInput _newInput;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _abilityAction;

    private float bulletTimer = 0f;
    public float bulletTimerOffset = 0.1f;
    
    private float bombTimer = 0f;
    public float bombTimerOffset = 5f;
    public GameObject bombPrefab;
    public UnityEvent onBombShot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _newInput  = new NewInput();
        _moveAction = _newInput.Player.Move;
        _moveAction.Enable();
        _shootAction = _newInput.Player.Shoot;
        _shootAction.Enable();
        _abilityAction = _newInput.Player.Ability;
        _abilityAction.Enable();
    }

    void FixedUpdate()
    {
        // Handle movement input from keyboard or left joystick
        movementInput = _moveAction.ReadValue<Vector2>();
        aimInput = _shootAction.ReadValue<Vector2>();
        
        // Normalize the direction to avoid faster diagonal movement
        movementInput = movementInput.normalized;
        
        
        // Check for shooting input
        if (aimInput != Vector2.zero && bulletTimer >= bulletTimerOffset)
        {
            Shoot(aimInput);
            bulletTimer = 0f;
        }
        
        // Move the player using the movement input vector
        if(movementInput != Vector2.zero)
        {
            rb.velocity = movementInput * movementSpeed;
            // Rotate player or weapon to face the movement direction
            if(aimInput == Vector2.zero)
            {
                float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if (_abilityAction.IsPressed() && isBombReady())
        {
            bombTimer = 0f;
            shootBomb();
        }
        
        bulletTimer += Time.fixedDeltaTime;
        bombTimer += Time.fixedDeltaTime;
    }
    
    void Shoot(Vector2 aimDirection)
    {
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        // Create the bullet and shoot it in the aiming direction
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    
        // Normalize aim direction to ensure bullets shoot correctly in all 360 degrees
        aimDirection = aimDirection.normalized;
    
        // Set bullet velocity
        bulletRb.velocity = aimDirection * bulletSpeed;
    
        // Rotate player or weapon to face the aim direction
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public bool isBombReady()
    {
        return bombTimer >= bombTimerOffset;
    }
    
    void shootBomb()
    {
        onBombShot.Invoke();
        // Create the bullet and shoot it in the aiming direction
        GameObject bomb = Instantiate(bombPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        
        Vector2 facingDirection = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        // Normalize the direction vector
        facingDirection = facingDirection.normalized;
        
        // Set bullet velocity
        bombRb.velocity = facingDirection * bulletSpeed;
        
    }
    
    
    
    // void Update()
    // {
    //     // Handle movement input from keyboard or left joystick
    //     movementInput.x = Input.GetAxisRaw("LeftStickHorizontal"); // Left joystick X or keyboard A/D or Left/Right arrow
    //     movementInput.y = Input.GetAxisRaw("LeftStickVertical");   // Left joystick Y or keyboard W/S or Up/Down arrow
    //     
    //     // Normalize the direction to avoid faster diagonal movement
    //     movementInput = movementInput.normalized;
    //     
    //     aimInput = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));
    //
    //
    //     // Check for shooting input
    //     if (aimInput != Vector2.zero)
    //     {
    //         Shoot(aimInput);
    //     }
    // }
    //
    // private void FixedUpdate()
    // {
    //     // Move the player using the movement input vector
    //     if(movementInput != Vector2.zero)
    //     {
    //         rb.velocity = movementInput * movementSpeed;
    //     }
    //     else
    //     {
    //         rb.velocity = Vector2.zero;
    //     }
    // }
    //
    // void Shoot(Vector2 aimDirection)
    // {
    //     // Create the bullet and shoot it in the aiming direction
    //     GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    //     Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    //
    //     // Normalize aim direction to ensure bullets shoot correctly in all 360 degrees
    //     aimDirection = aimDirection.normalized;
    //
    //     // Set bullet velocity
    //     bulletRb.velocity = aimDirection * bulletSpeed;
    //
    //     // Rotate player or weapon to face the aim direction
    //     float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    // }
    
    
}
