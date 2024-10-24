using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float WallJumpForce;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float wallSlidingSpeed;
    
    [Header("Dash Settings")]
    [SerializeField] private float DashForce;
    private bool canDash;
    private bool isDashing;
    [SerializeField] private float dashingDuration = 1f;
    [SerializeField] float dashingCD = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCD;
    private float horInput;
    private bool isWallSliding;

   
    

    // Start is called before the first frame update
    void Start()
    {
        //References
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) { return; }
        //player controller interactions
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.K))
        {
            StartCoroutine(Dash());
            canDash = true;

        }
        horInput = Input.GetAxis("Horizontal");
        //check if going up or down
        float verVelocity = rb.velocity.y;

        //movement Left and Right
        rb.velocity = new Vector2(horInput * Speed, rb.velocity.y);
        //sprite flipping
        if (horInput != 0) sr.flipX = (horInput < 0);

        //Set animator
        anim.SetBool("Run", horInput != 0);
        anim.SetBool("Grounded", isGrounded());
        anim.SetBool("Rising", verVelocity > 0);
        anim.SetBool("Falling", verVelocity < 0);
        anim.SetBool("Dashing", isDashing);
        anim.SetBool("OnWall", isWalled());

        wallSlide();


       
    }
    void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            anim.SetTrigger("Jump");
        }
        else if (isWalled() && !isGrounded())
        {
            if(horInput == 0)
            {
                
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * WallJumpForce, JumpForce);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCD = 0;
        }       
    }
    //DASHING
    private IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = new Vector2(horInput * DashForce, 0);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashingDuration);
        rb.gravityScale = 1;
        isDashing = false;

    }
    //GROUNDED
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
    //WALLED
    private bool isWalled()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.5f, wallLayer);

        return raycastHit.collider != null;
    }
    //wallsliding ideas
    private void wallSlide()
    {
        if(isWalled() && !isGrounded() && horInput !=0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, - wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    //ATTACK
    public bool canAttack()
    {
        return isGrounded() && !isWalled();
    }
}
