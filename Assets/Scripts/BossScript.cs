using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject player;
    private float playerDistance;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 0.5f;
    
    public float bulletOffset = 5f;
    public float bulletTimer = 0f;
    public float bulletSpeed = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPointA;
    public Transform bulletSpawnPointB;
    private Transform [] bulletSpawns = new Transform[2];
    
    public GameObject shield;
    public float shieldTimer = 0f;
    public float shieldTime = 5f;
    private float shieldRadius;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boss Hit");
    }

    private void Start()
    {
        bulletSpawns[0] = bulletSpawnPointA;
        bulletSpawns[1] = bulletSpawnPointB;
        shieldRadius = shield.GetComponent<CircleCollider2D>().radius * shield.transform.localScale.x;
    }

    private void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= bulletOffset)
        {
            ShootBullets();
            bulletTimer = 0f;
            
        }
        moveTowardsPlayer();
        checkShieldTimer();
    }

    private void moveTowardsPlayer()
    {
        playerDistance = Vector2.Distance(player.transform.position, transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        // Create the target rotation from the angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ShootBullets()
    {
        foreach (var bulletSpawn in bulletSpawns)
        {
            float angle = Mathf.Atan2(Vector2.down.x, Vector2.down.y) * Mathf.Rad2Deg;
        
            // Create the bullet and shoot it in the aiming direction
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            
            // Set bullet velocity as per rotation;
            bulletRb.velocity = -bulletSpawn.up * bulletSpeed;
        }
    }

    public void OnBombShot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer > shieldRadius)
        {
            shield.SetActive(true);
            shieldTimer = 0f;
        }
    }

    void checkShieldTimer()
    {
        shieldTimer += Time.deltaTime;
        if (shieldTimer >= shieldTime)
        {
            shield.SetActive(false);
        }
    }
}
