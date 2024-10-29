using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private Transform groundCheck;
    [SerializeField, Range(0.01f,1)] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        //creat ground check object, assume pivot is at bottom center
        if(!groundCheck)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.transform.SetParent(transform);
            newGameObject.transform.localPosition = Vector3.zero;
            newGameObject.name = "GroundCheck";
            groundCheck = newGameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
