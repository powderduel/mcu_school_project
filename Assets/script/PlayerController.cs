using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    [SerializeField]
    private float  dashingVelocity=14f;
    [SerializeField]
    private float  dashingTime=1f;
    [SerializeField]
    private SpriteRenderer sprite;

    private Vector2 dashingDirection;
    


    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private Animator anim;

    private int facingDirection = 1;

    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    private bool isWalking;
    private bool isAttack;
    public bool isDashing;
    private bool canDash=true;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;

    private Vector2 slopeNormalPerp;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsuleColliderSize = cc.size;
    }

    private void Update()
    {
        CheckInput();
        changAnimator();
        CheckDashing();

    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        ApplyMovement();
        trail();


    }
    private void trail()
    {
        if(isDashing==true&&isGrounded==false)
        {
		GameObject go = new GameObject("New Sprite");
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
		SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        go.AddComponent<SelfDelete>();
		renderer.sprite = sprite.sprite;
        renderer.sortingLayerName = "4-player";
        }
    }
    private void changAnimator()
    {
        if(xInput ==1 || xInput == -1)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isGrounded", isGrounded);
        if(Input.GetButtonDown("Fire1"))
        {
            attack();
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Death");
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput == 1 && facingDirection == -1)
        {
            
            Flip();
        }
        else if (xInput == -1 && facingDirection == 1)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonDown("Dash")&&canDash)
        {
            Dash();
            Debug.Log("Dash");
        }



    }
    private void attack()
    {
        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            StartCoroutine(delay(enemy));
            Debug.Log("hit"+enemy.name);
            

        }
    }
    IEnumerator delay(Collider2D enemy){
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<Ground_AI>().been_attacked(Status.attack);
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void CheckDashing()
    {
        if(!isDashing)
        {
            canDash = true;
        }
        anim.SetBool("isDashing", isDashing);
    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if(isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }

    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {      
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;            

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }                       

            lastSlopeAngle = slopeDownAngle;
           
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
        }
    }   

    private void ApplyMovement()
    {
        if (isGrounded && !isOnSlope && !isJumping) 
        {
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping)
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded) //If in air
        {
            newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
            rb.velocity = newVelocity;
        }
        
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void Dash()
    {
        isDashing = true;
        canDash = false;
        dashingDirection= new Vector2(xInput,Input.GetAxisRaw("Vertical"));
        if(dashingDirection==Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0f);
        }
        StartCoroutine(stopDashing());
        
        if(isDashing)
        {
            rb.velocity = dashingDirection * dashingVelocity;
            return;
        }

    }
    private IEnumerator stopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

    }


}