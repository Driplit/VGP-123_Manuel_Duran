using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       //References
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement Left and Right
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*Speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space))
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);

    }
}
