using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Stats")]
    public float jumpForce;
    public float speed;
    public float jumpTime;
    private float jumpTimeCounter;

    private bool isGrounded, isJumping;

    [Header("Other Stuff")]
    public bool blocking;
    public Transform feetpos;
    public float checkRadius;
    public LayerMask whatIsGround;

    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {        
        #region Jumping
        isGrounded = Physics2D.OverlapCircle(feetpos.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown("w"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKey("w") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce + 2);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp("w"))
        {
            isJumping = false;
        }
        #endregion

        #region Rolling
        if (Input.GetKey("r")) { blocking = true; } //starts blocking  
        else if (Input.GetKeyUp("r")) { blocking = false; } //stops blocking

        if (blocking && Input.GetKeyDown("a"))
        {
            rb.AddForce(Vector2.left * speed * 900);
        }
        if (blocking && Input.GetKeyDown("d"))
        {
            rb.AddForce(Vector2.right * speed * 900);
        }
        #endregion

        #region Animation stuff

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d") && Input.GetKey("a") && blocking == false) // Right
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stops player
            transform.localScale = new Vector3(4, transform.localScale.y); // Makes the player look right
        }
        else if (Input.GetKey("d") && blocking == false) // Right
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //Moves the player right
            transform.localScale = new Vector3(4, transform.localScale.y); // Makes the player look right
        }
        else if (Input.GetKey("a") && blocking == false)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); //Moves the player left
            transform.localScale = new Vector3(-4, transform.localScale.y); // Makes the player look left
        }        
        else
        {
            if (GetComponent<CombatSystem>().attacking == false)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
