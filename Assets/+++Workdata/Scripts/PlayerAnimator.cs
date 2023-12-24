using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector2 moveDirection = Vector2.down;
    private float speed = 0;

    private void Awake()
    {
        animator.SetFloat("MoveDirX", moveDirection.x);
        animator.SetFloat("MoveDirY", moveDirection.y);
        animator.SetFloat("Speed", speed);
    }

    public void SetLookDirection(Vector2 newDirecton)
    {
        if (newDirecton.sqrMagnitude <= 0)
        {
            return;
        }
        
        moveDirection = newDirecton;
        animator.SetFloat("MoveDirX", moveDirection.x);
        animator.SetFloat("MoveDirY", moveDirection.y);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        animator.SetFloat("Speed", speed);
    }
}
