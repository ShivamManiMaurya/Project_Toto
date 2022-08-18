using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private AudioClip _bounceSfx;
    [SerializeField] private float _bounceSfxVolume = 1f;

    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    BoxCollider2D totoFeetCollider;
    AudioSource playerAudioSource;
    


    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
        playerAudioSource = GetComponent<AudioSource>();
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
        if (totoHasVelocity && Mathf.Abs(totoRigidbody.velocity.y) <= 0.2 &&
            totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            //StartCoroutine(FootStepSound());
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.Play();
            }
        }


        // Jumping Animation
        if (Mathf.Abs(totoRigidbody.velocity.y) > 0.8)
        {
            ResumeAnimation();
            totoAnimator.SetBool("isJumping", true);
            totoAnimator.SetBool("isRunning", false);
        }

        if (totoRigidbody.velocity.y == 0)
        {
            ResumeAnimation();
            totoAnimator.SetBool("isJumping", false);
        }

        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            ResumeAnimation();
            totoAnimator.SetBool("isJumping", false);
        }

        // Climbing Animation
        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && Mathf.Abs(totoRigidbody.velocity.y) > 0.8f
            && !totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            ResumeAnimation();
            totoAnimator.SetBool("isClimbing", true);
            totoAnimator.SetBool("isJumping", false);
        }
        else if ((totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) && totoRigidbody.velocity.y == 0) ||
                  !totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            ResumeAnimation();
            totoAnimator.SetBool("isClimbing", false);
        }
        else if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && totoRigidbody.velocity.y == 0f &&
                !totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) )
        {
            PauseClimbAnimation();
        }


        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Bouncing")))
        {
            playerAudioSource.PlayOneShot(_bounceSfx, _bounceSfxVolume);
        }


    }

    public void PauseClimbAnimation()
    {
        totoAnimator.speed = 0;
    }

    public void ResumeAnimation()
    {
        totoAnimator.speed = 1;
    }


}
