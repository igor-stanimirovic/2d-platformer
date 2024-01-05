using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float jumpForce = 7;
    [SerializeField] float movementSpeed = 4;
    [SerializeField] LayerMask ground;
    Rigidbody2D rb;
    BoxCollider2D boxColl;
    Animator anim;
    SpriteRenderer spriteRend;
    float directionX;
    bool doubleJump;
    enum MovementState {idle, running, jumping, falling};


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        directionX = Input.GetAxis("Horizontal");
        transform.position += transform.right * directionX * movementSpeed * Time.deltaTime;

        // Jumping and double jumping conditions
        if (!Input.GetButtonDown("Jump") && IsOnGround())
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsOnGround() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }

        UpdateAnimationStatus();

    }


    // Animation runing & idle switcher method
    void UpdateAnimationStatus()
    {
        MovementState state;

        // Running animation contoller
        if (directionX > 0)
        {
            state = MovementState.running;
            spriteRend.flipX = false;
        }

        else if (directionX < 0)
        {
            state = MovementState.running;
            spriteRend.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // Jumping and Falling animation controller
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state",(int)state);
    }


    bool IsOnGround()
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0, Vector2.down, 0.1f, ground);
    }

}
