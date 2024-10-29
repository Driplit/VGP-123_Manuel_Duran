using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    GroundCheck gc;
    [SerializeField, Range(2,5)] public float jumpHeight = 5;
    [SerializeField, Range(20,100)] public float jumpFallForce = 50;

    float timeHeld;
    float maxHoldTime = 0.5f;
    float jumpInputTime = 0;
    float calculatedJumpForce;

    bool jumpCancelled;
    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        pc= GetComponent<PlayerController>();
        gc = pc.GetComponent<GroundCheck>();
        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump")) jumpInputTime = Time.time;
        if (Input.GetButton("Jump")) timeHeld += Time.deltaTime;
        if (Input.GetButtonUp("Jump"))
        {
            timeHeld = 0;
            jumpInputTime = 0;
        }
        if(jumpInputTime !=0 && (jumpInputTime + timeHeld)<(jumpInputTime + maxHoldTime))
        {
            if(pc.isGrounded)
            {

            }
        }

        if (Input.GetButtonDown("Jump")&& pc.isGrounded)
        {
            rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode.Impulse);
        }
    }
}
