using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;

    private float size_x, horizontalVelocity;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size_x = transform.localScale.x;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        horizontalVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", horizontalVelocity);

        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //Moves the player right
            transform.localScale = new Vector3(size_x, transform.localScale.y); // Makes the player look right
        }
        else if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); //Moves the player left
            transform.localScale = new Vector3(-size_x, transform.localScale.y); // Makes the player look left
        }
    }
}

