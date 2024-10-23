using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;


    //Movement variables
    [Range(3, 10)]
    public float speed = 5.5f;
    [Range(3, 10)]
    public float jumpForce = 10f;

    //Ground Check Variables
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //Ground Check Initalization
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        



        float hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);


        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        if(IsFalling())
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
        if (IsJumping())
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }
        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        //alternate way to sprite flip
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        anim.SetFloat("speed", Mathf.Abs(hInput));
        
    }

    
    bool IsFalling()
    {
        return rb.velocity.y < 0;
    }
    bool IsJumping()
    {
        return rb.velocity.y > 0;
    }
}
