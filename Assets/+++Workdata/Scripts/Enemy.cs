using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthManager healthManager;
    private const int BulletIncomeDamage = 1;
    
    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            healthManager.GetDamage(BulletIncomeDamage);
            Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        if (healthManager.isDead)
        {
            Destroy(gameObject);
        }
    }
}
