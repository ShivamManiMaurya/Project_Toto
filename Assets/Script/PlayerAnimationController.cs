using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    BoxCollider2D totoFeetCollider;


    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        SetAnimation();
    }


    // Setting Animations
    void SetAnimation()
    {
        // Running Animation
        bool totoHasVelocity = Mathf.Abs(totoRigidbody.velocity.x) > 0.5f;
        totoAnimator.SetBool("isRunning", totoHasVelocity);


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

        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            totoAnimator.SetBool("isJumping", false);
        }

        // Climbing Animation
        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && Mathf.Abs(totoRigidbody.velocity.y) > 0.8f
            && !totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            totoAnimator.SetBool("isClimbing", true);
            totoAnimator.SetBool("isJumping", false);
        }
        else if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) && totoRigidbody.velocity.y == 0)
        {
            totoAnimator.SetBool("isClimbing", false);
        }

        // Death Animation



    }


}
