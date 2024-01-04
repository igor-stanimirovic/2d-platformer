using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRend;
    float directionX;
    [SerializeField] float jumpForce = 7;
    [SerializeField] float movementSpeed = 4;

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
        if (directionX > 0)
        {
            anim.SetBool("running", true);
            spriteRend.flipX = false;
        }

        else if (directionX < 0)
        {
            anim.SetBool("running", true);
            spriteRend.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

}
