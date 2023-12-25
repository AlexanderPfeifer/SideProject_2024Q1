using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5;
    [SerializeField] private GameObject enemyDeathScreen;
    [SerializeField] private ParticleSystem deathParticle;

    private HealthManager healthManager;
    private Rigidbody2D rb;

    private const int BulletIncomeDamage = 1;
    
    private Vector2 randomSphereDir;
    public bool enemyCanMove;
    
    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Changes the moving Direction when colliding with something
    //When colliding with a bullet, looses health
    private void OnCollisionEnter2D(Collision2D col)
    {
        ChangeSphereDirection();
        
        if (col.gameObject.GetComponent<Bullet>())
        {
            healthManager.GetDamage(BulletIncomeDamage);
            Destroy(col.gameObject);
            enemyCanMove = true;
        }
    }

    private void Update()
    {
        if (healthManager.isDead)
        {
            deathParticle.transform.position = transform.position;
            deathParticle.Play();
            enemyDeathScreen.SetActive(true);
            Time.timeScale = 0.25f;
            Destroy(gameObject);
        }
    }

    //When the enemy gets stuck, so it doesn't move in any way, it changes direction
    private void FixedUpdate()
    {
        if (enemyCanMove)
        {
            StartMoving();
        }

        if (rb.velocity == Vector2.zero)
        {
            ChangeSphereDirection();
        }
    }

    //Starts moving in the direction of the random Sphere Input of the method below 
    private void StartMoving()
    {
        rb.velocity = randomSphereDir * (enemySpeed * Time.deltaTime);
    }

    //Changes the direction of the enemy in a spherical manner
    public void ChangeSphereDirection()
    {
        randomSphereDir = Random.onUnitSphere;
    }
}
