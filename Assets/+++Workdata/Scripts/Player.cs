using System;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float playerSpeed;
    private Vector2 moveDirection;
    private PlayerAnimator playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
    }

    private void Update()
    {
        MovementUpdate();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * playerSpeed, ForceMode2D.Force);
        
        playerAnimator.SetSpeed(rb.velocity.magnitude);
    }

    private void MovementUpdate()
    {
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        
        playerAnimator.SetLookDirection(moveDirection);
    }
}
