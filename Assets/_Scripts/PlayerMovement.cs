using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] float jumpForce = 7;
    [SerializeField] float movementSpeed = 4;
    Rigidbody2D rb;
    BoxCollider2D boxColl;
    Animator anim;
    SpriteRenderer spriteRend;
    float xBoundMin_Level1 = -5.56f;
    float xBoundMax_Level1 = 70f;
    float xBoundMin_Level2 = -5.49f;
    float xBoundMax_Level2 = 68.467f;
    float xBoundMin;
    float xBoundMax;
    float clampedX;
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
        SetBoundaries();
    }

    // Update is called once per frame
    void Update()
    {

        directionX = Input.GetAxis("Horizontal");
        // Calculate the new position
        Vector3 newPosition = transform.position + transform.right * directionX * movementSpeed * Time.deltaTime;

        // Clamp the new position within the specified boundaries
        clampedX = Mathf.Clamp(newPosition.x, xBoundMin, xBoundMax);

        // Update the player's position only on the x-axis
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);


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
                jumpSound.Play();
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

    // Method for setting appropriate boundaries depending on the Scene
    void SetBoundaries()
    {
        // Check the current scene and set boundaries accordingly
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 1")
        {
            xBoundMin = xBoundMin_Level1;
            xBoundMax = xBoundMax_Level1;
        }
        else if (currentSceneName == "Level 2")
        {
            xBoundMin = xBoundMin_Level2;
            xBoundMax = xBoundMax_Level2;
        }
    }

}
