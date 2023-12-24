using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 5;
    private Player player;
    private Vector2 playerCurrentMoveDir;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<Player>();

        playerCurrentMoveDir = player.moveDirection;
    }

    private void Update()
    {
        rb.velocity = new Vector2(Mathf.Round(playerCurrentMoveDir.x) * bulletSpeed, Mathf.Round(playerCurrentMoveDir.y) * bulletSpeed);
    }
}
