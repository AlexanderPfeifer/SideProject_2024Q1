using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 5;
    private PlayerAnimator playerAnimator;
    private Vector2 playerCurrentMoveDir;
    
    //Gets the direction of where the player is looking
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = FindObjectOfType<PlayerAnimator>();

        playerCurrentMoveDir = playerAnimator.moveDirection;
    }

    //Lets the bullet fly in the direction of where player was last looking
    private void Update()
    {
        rb.velocity = new Vector2(Mathf.Round(playerCurrentMoveDir.x) * bulletSpeed, Mathf.Round(playerCurrentMoveDir.y) * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
