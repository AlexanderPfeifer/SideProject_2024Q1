using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimator playerAnimator;
    private HealthManager healthManager;
    
    private Vector2 moveDirection;

    [SerializeField] private GameObject playerDeathScreen;
    [SerializeField] private float playerSpeed;

    private const int EnemyCollisionDamage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        healthManager = GetComponent<HealthManager>();
        playerDeathScreen.SetActive(false);
    }

    private void Update()
    {
        MovementUpdate();

        if (healthManager.isDead)
        {
            playerDeathScreen.SetActive(true);
        }
    }
    
    //I'm adding force to the Rigidbody by multiplying its speed and move direction.
    //In the animator I'm setting the speed to the rigidbody magnitude. Magnitude returns the length of the direction Vector for velocity.
    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * playerSpeed, ForceMode2D.Force);
        
        playerAnimator.SetSpeed(rb.velocity.magnitude);
    }

    //Here I use the Vertical and Horizontal Input to know which button of wasd was pressed and I set this equal to where the player should move.
    //I normalize the movement so the length of the direction vector stays the same for when holding down a and w for example. Otherwise it multiplies.
    private void MovementUpdate()
    {
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        
        playerAnimator.SetLookDirection(moveDirection);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            healthManager.GetDamage(EnemyCollisionDamage);
        }
    }
}
