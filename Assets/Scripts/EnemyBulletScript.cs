using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject boss;
    private Collider2D[] bossColliders;

    private void Awake()
    {
        bossColliders = boss.GetComponentsInChildren<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        foreach (var bossCollider in bossColliders)
        {
            Physics2D.IgnoreCollision(bossCollider, GetComponent<Collider2D>());
        }
        if (other.gameObject.layer != 8)
        {
            Destroy(gameObject);
        }
        
    }
}
