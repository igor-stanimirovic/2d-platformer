using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float jump = 2;
    [SerializeField] float movementSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0,jump),ForceMode2D.Impulse);
        }
    }
}
