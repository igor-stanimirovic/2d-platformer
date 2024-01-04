using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 7;
    [SerializeField] float movementSpeed = 4;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRend;
    float directionX;
    enum MovementState { idle, running, jumping, falling};

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal");

        transform.position += transform.right * directionX * movementSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
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

}
