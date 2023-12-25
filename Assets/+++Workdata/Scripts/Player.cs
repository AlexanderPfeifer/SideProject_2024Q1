using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimator playerAnimator;
    private HealthManager healthManager;
    private Camera playerCam;
    private Enemy enemy;

    [HideInInspector] public Vector2 moveDirection;

    [SerializeField] private GameObject playerDeathScreen;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private GameObject door;

    private const int EnemyCollisionDamage = 1;
    private const float BulletShootingDelay = 0.35f;
    private float currentBulletShootingDelay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        healthManager = GetComponent<HealthManager>();
        playerDeathScreen.SetActive(false);
        door.SetActive(false);
        playerCam = FindObjectOfType<Camera>();
        enemy = FindObjectOfType<Enemy>();
        Time.timeScale = 1;
    }

    //Besides Updating multiple methods, the cameras transform is set equal to the players transform
    private void Update()
    {
        MovementUpdate();

        SpawnBullet();

        if (healthManager.isDead)
        {
            playerDeathScreen.SetActive(true);
            Time.timeScale = 0.25f;
            Destroy(gameObject);
        }

        var transformPositionCam = transform.position;
        transformPositionCam.z = -10;
        playerCam.transform.position = transformPositionCam;
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
    
    //Instantiates a BulletPrefab on player position 
    private void SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBulletShootingDelay <= 0)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            currentBulletShootingDelay = BulletShootingDelay;
        }

        currentBulletShootingDelay -= Time.deltaTime;
    }

    //Subtracts the the the currentHealth of Player with collisionDamage
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            healthManager.GetDamage(EnemyCollisionDamage);
            enemy.enemyCanMove = true;
            enemy.ChangeSphereDirection();
        }
    }

    //Sets Door element active when on invisible collider enter
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<BoxCollider2D>())
        {
            door.SetActive(true);
        }
    }
}
