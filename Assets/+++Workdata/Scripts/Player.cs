using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimator playerAnimator;
    private HealthManager healthManager;

    [HideInInspector] public Vector2 moveDirection;

    [SerializeField] private GameObject playerDeathScreen;
    [SerializeField] private float playerSpeed;

    private const int EnemyCollisionDamage = 1;

    [SerializeField] private Transform bulletPrefab;

    private Camera playerCam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        healthManager = GetComponent<HealthManager>();
        playerDeathScreen.SetActive(false);
        playerCam = FindObjectOfType<Camera>();
    }

    //Besides Updating multiple methods, the cameras transform is set equal to the players transform
    private void Update()
    {
        MovementUpdate();

        SpawnBullet();

        if (healthManager.isDead)
        {
            playerDeathScreen.SetActive(true);
        }

        var transformPositionCam = playerCam.transform.position;
        transformPositionCam = transform.position;
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

    //Subtracts the the the currentHealth of Player with collisionDamage
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            healthManager.GetDamage(EnemyCollisionDamage);
        }
    }

    //Instantiates a BulletPrefab on player position 
    private void SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
