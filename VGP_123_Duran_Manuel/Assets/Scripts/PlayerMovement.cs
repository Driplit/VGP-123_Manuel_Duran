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
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCD;
    private float horInput;
    private float wallSlidingSpeed;
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
        horInput = Input.GetAxis("Horizontal");
        //going up or down
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
        anim.SetBool("Dashing", isGrounded() && !isGrounded());
        anim.SetBool("OnWall", isWalled());


        //wall jump logic
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
                //chach back to the varible names i made (WallJumpForce, JumpForce)
                rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCD = 0;
        }
            
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
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
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void wallSlide()
    {
        if(isWalled() && !isGrounded() && horInput !=0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, - wallSligingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    //ATTACK
    // public bool canAttack()
    //    { return horInput == 0&& isGrounded() && !onWall(); }

}
