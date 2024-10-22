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
    public float jumpForce = 3f;

    //Ground Check Variables
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;
    bool isGrounded = false;
    Transform groundCheck;

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
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();



        float hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        //alternate way to sprite flip
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }
        else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
