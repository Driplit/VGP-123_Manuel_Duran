using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float Speed;
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
        //player controller interaction
        if (Input.GetKey(KeyCode.K))
        {
            anim.SetBool("isDashing", true);

        }
        if (Input.GetKey(KeyCode.J))
        {
            anim.SetBool("isAttacking", true);
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
        anim.SetBool("OnWall", isWalled());




        Debug.Log(isWalled());

    }
    //ATTACKING
    public void endAttack()
    {

        anim.SetBool("isAttacking", false);
    }
   
    //DASHING
    public void endDash()
    {
        anim.SetBool("isDashing", false);
    }
    //GROUNDED
  /* void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0) isGrounded = gc.IsGrounded();
        }
    }*/
    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
    private bool isWalled()
    {
        if (horInput > 0)
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);
            return raycastHit.collider != null;

        }
        else
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
            return raycastHit.collider != null;

        }
    }
}
