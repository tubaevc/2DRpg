using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Vector2 movement;
   [SerializeField] private Animator anim;
    private bool facingRight = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float jumpForce;


    private bool isGrounded;
    public float groundCheckRadius = 0.2f;
    
 
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            anim.SetBool("Run", true);
           //  Debug.Log("run animation active");
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle",true);
          //  Debug.Log("run animation deactive");
        }


        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Idle", true);
        }

        if (moveInput < 0 && facingRight)
        {
            Flip();
            facingRight = !facingRight;
        }
        else if (moveInput > 0 && !facingRight)
        {
            Flip();
            facingRight = !facingRight;

        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("jump");
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    
    }

    private void Flip()
    {
        transform.Rotate(0, 180f, 0);
    }

    




}