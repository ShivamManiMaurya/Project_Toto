using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float totoSpeed = 5;
    [SerializeField] float totoJumpSpeed = 5;
    [SerializeField] float totoClimbSpeed = 5;

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

        bool val = Mathf.Abs(totoRigidbody.velocity.x) > Mathf.Epsilon;
        totoAnimator.SetBool("isRunning", val);

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
        Debug.Log(totoRigidbody.transform.localScale);

        facingRight = !facingRight;
    }



    // Jumping Part
    void OnJump(InputValue value)
    {
        if (!totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }
   
        if (value.isPressed)
        {
            totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed); 
        }
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

        Debug.Log(totoRigidbody.velocity);

    }



    // Setting Animaitons
    void SetAnimation()
    {
        if (totoRigidbody.velocity.y > 0.8)
        {
            totoAnimator.SetBool("isJumping", true);
            totoAnimator.SetBool("isRunning", false);
        }

        if (totoRigidbody.velocity.y == 0)
        {
            totoAnimator.SetBool("isJumping", false);
        }

        if (!totoCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
        }

    }





}