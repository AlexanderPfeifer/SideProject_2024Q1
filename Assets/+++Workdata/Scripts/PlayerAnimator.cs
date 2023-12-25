using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Vector2 moveDirection = Vector2.down;
    private float speed;

    //Here I set the floats of direction and speed of the player equal to the animation floats.
    private void Awake()
    {
        animator.SetFloat("MoveDirX", moveDirection.x);
        animator.SetFloat("MoveDirY", moveDirection.y);
        animator.SetFloat("Speed", speed);
    }

    //I ask if the Magnitude has changed, if it doesn't, then it returns, otherwise a new direction is set equal to the animation parameter of direction.
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
