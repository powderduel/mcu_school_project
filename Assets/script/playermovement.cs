using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontal;
    public float speed ;
    public float jump ;
    private bool isFacingRight = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        horizontal=Input.GetAxisRaw("Horizontal");
        flip();
        if(Input.GetButtonDown("Jump")&&isGrounded())
        {
            rb.velocity=new Vector2(rb.velocity.x,jump);
        }
        if(Input.GetButtonUp("Jump")&&rb.velocity.y>0)
        {
            rb.velocity=new Vector2(rb.velocity.x,rb.velocity.y*0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity=new Vector2(horizontal*speed,rb.velocity.y);
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void flip()
    {
        if(isFacingRight&&horizontal<0||!isFacingRight&&horizontal>0)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
