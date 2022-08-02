using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float totoSpeed = 5;
    [SerializeField] float totoJumpSpeed = 5;
    [SerializeField] float totoClimbSpeed = 5;
    [SerializeField] LayerMask platformLayer;

    bool facingRight = true;

    Vector2 moveInput;
    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    CapsuleCollider2D totoCapsuleCollider;
    float gravityValueAtStart;
    

    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
        totoCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityValueAtStart = totoRigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        SetAnimation();
        ClimbLadder();
    }



    // Movement Part
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();   
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(totoSpeed * moveInput.x, totoRigidbody.velocity.y);
        totoRigidbody.velocity = playerVelocity;

        // This method also works, but it continuously changes the scale.
        //if (Mathf.Abs(totoRigidbody.velocity.x) > 0)
        //{
        //    Debug.Log(totoRigidbody.transform.localScale);
        //    totoRigidbody.transform.localScale = new Vector2(Mathf.Sign(totoRigidbody.velocity.x), 1f);
        //}

        if (totoRigidbody.velocity.x > 0 && !facingRight)
        {
            FlipPlayer();
        }
        if (totoRigidbody.velocity.x < 0 && facingRight)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        Vector3 currentScale = totoRigidbody.transform.localScale;
        currentScale.x *= -1;
        totoRigidbody.transform.localScale = currentScale;
 
        facingRight = !facingRight;
    }



    // Jumping Part
    void OnJump(InputValue value)
    {
        if (!totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }
   
        if (value.isPressed && IsGrounded())
        {
            totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed); 
        }
    }

    private bool IsGrounded()
    {
        // code from codeMonkey for boxRayCast to check player is grounded or not
        float hightOfBoxCast = 1f;
        RaycastHit2D boxRayCastHit = Physics2D.BoxCast(totoCapsuleCollider.bounds.center, totoCapsuleCollider.bounds.size, 
                                               0f, Vector2.down, hightOfBoxCast, platformLayer);

        //Color rayColor;
        //if (boxRayCastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(totoCapsuleCollider.bounds.center + new Vector3(totoCapsuleCollider.bounds.extents.x, 0),
        //    Vector2.down * (totoCapsuleCollider.bounds.extents.y + hightOfBoxCast), rayColor);
        //Debug.DrawRay(totoCapsuleCollider.bounds.center - new Vector3(totoCapsuleCollider.bounds.extents.x, 0),
        //    Vector2.down * (totoCapsuleCollider.bounds.extents.y + hightOfBoxCast), rayColor);
        //Debug.DrawRay(totoCapsuleCollider.bounds.center - new Vector3(totoCapsuleCollider.bounds.extents.x, 
        //    totoCapsuleCollider.bounds.extents.y + hightOfBoxCast), Vector2.right * (totoCapsuleCollider.bounds.extents.x), rayColor);






        return boxRayCastHit.collider != null;
    }


    
    // Climbing Part
    void ClimbLadder()
    {
        if (!totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            totoRigidbody.gravityScale = gravityValueAtStart;
            return;
        }

        Vector2 climbVelocity = new Vector2(totoRigidbody.velocity.x, moveInput.y * totoClimbSpeed);
        totoRigidbody.velocity = climbVelocity;

        totoRigidbody.gravityScale = 0;
    }



    // Setting Animations
    void SetAnimation()
    {
        // Running Animation
        bool val = Mathf.Abs(totoRigidbody.velocity.x) > Mathf.Epsilon;
        totoAnimator.SetBool("isRunning", val);

        // Jumping Animation
        if (Mathf.Abs(totoRigidbody.velocity.y) > 0.8)
        {
            totoAnimator.SetBool("isJumping", true);
            totoAnimator.SetBool("isRunning", false);
        }

        if (totoRigidbody.velocity.y == 0)
        {
            totoAnimator.SetBool("isJumping", false);
        }

        if (totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            totoAnimator.SetBool("isJumping", false);
        }

        // Climbing Animation
        if (totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && Mathf.Abs(totoRigidbody.velocity.y) > 0.8f)
        {
            totoAnimator.SetBool("isClimbing", true);
            totoAnimator.SetBool("isJumping", false);
            if (totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                totoAnimator.SetBool("isClimbing", false);
            }
        }
        else if (totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) && totoRigidbody.velocity.y == 0)
        {
            totoAnimator.SetBool("isClimbing", false);
        }

        
      

    }





}
