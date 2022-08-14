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
    [SerializeField] GameObject projectile;
    [SerializeField] Transform catapultPosition;


    // Audio Clips and volume
    [SerializeField] private AudioClip _projectileShootSfx;
    [SerializeField] private float _projectileShooteSfxVolume = 1f;
    [SerializeField] private AudioClip _jumpSfx;
    [SerializeField] private float _jumpVolume = 1f;
   

    bool facingRight = true;
    float gravityValueAtStart;

    Vector2 moveInput;
    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    BoxCollider2D totoFeetCollider;
    GameSession gameSession;
    AudioSource playerAudioSource;


    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
        gravityValueAtStart = totoRigidbody.gravityScale;
        gameSession = FindObjectOfType<GameSession>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If Player is Dead then these functions don't work
        if (gameSession.playerLives > 0 && !PauseMenu.GameIsPaused)
        {
            Run();
            ClimbLadder();
        }
        
        
    }

    // Player got Damage
    public void DamageKnockBack(int damageAmount)
    {
        HeartsHealthVisual.heartsHealthSystemStatic.Damage(damageAmount);
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
        if (!totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) {

            if (value.isPressed && (gameSession.playerLives > 0) && totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
            {
                totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed);
            }

            return; 
        }
   
        if (value.isPressed && IsGrounded() && (gameSession.playerLives > 0) 
            && !PauseMenu.GameIsPaused) 
        {
            totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed);
            //SoundManager.Instance.PlaySound(_jumpSfx, _jumpVolume);
            playerAudioSource.PlayOneShot(_jumpSfx, _jumpVolume);
        }
    }


    // This method is used from the tutorial of codeMonkey, for boxRayCast to check player is grounded or not
    private bool IsGrounded()
    {
        float hightOfBoxCast = 1f;
        RaycastHit2D boxRayCastHit = Physics2D.BoxCast(totoFeetCollider.bounds.center, totoFeetCollider.bounds.size, 0f,
                                                        Vector2.down, hightOfBoxCast, platformLayer);

        return boxRayCastHit.collider != null;
    }


    
    // Climbing Part
    void ClimbLadder()
    {
        if (!totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            totoRigidbody.gravityScale = gravityValueAtStart;
            return;
        }

        Vector2 climbVelocity = new Vector2(totoRigidbody.velocity.x, moveInput.y * totoClimbSpeed);
        totoRigidbody.velocity = climbVelocity;

        totoRigidbody.gravityScale = 0;
    }




    // Shooting Projectile
    void OnFire(InputValue value)
    {
        if ((gameSession.playerLives <= 0)) { return; }

        if (value.isPressed && !PauseMenu.GameIsPaused)
        {
            Invoke("InstantiateProjectile", 0.5f);
            totoAnimator.SetTrigger("Attack");
        }
    }


    private void InstantiateProjectile()
    {
        Instantiate(projectile, catapultPosition.position, transform.rotation);
        SoundManager.Instance.PlaySound(_projectileShootSfx, _projectileShooteSfxVolume);
    }



}
