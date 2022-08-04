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
    [SerializeField] int damageAmount = 4;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform catapultPosition;

    bool facingRight = true;
    float gravityValueAtStart;

    Vector2 moveInput;
    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    CapsuleCollider2D totoBodyCollider;
    BoxCollider2D totoFeetCollider;
    HeartsHealthVisual heartsHealthVisual;


    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
        totoBodyCollider = GetComponent<CapsuleCollider2D>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
        gravityValueAtStart = totoRigidbody.gravityScale;
        heartsHealthVisual = FindObjectOfType<HeartsHealthVisual>();

    }

    void Update()
    {
        // If Player is Dead then these functions don't work
        if (!heartsHealthVisual.isDead)
        {
            Run();
            ClimbLadder();
        }
        
    }

    // Player got Damage
    public void DamageKnockBack(int damageAmount)
    {
        //transform.position += knockbackDir * knockbackDistance;
        HeartsHealthVisual.heartsHealthSystemStatic.Damage(damageAmount);
    }

    // Player got Healed
    public void Heal(int healAmount)
    {
        HeartsHealthVisual.heartsHealthSystemStatic.Heal(healAmount);
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

            if (value.isPressed && !heartsHealthVisual.isDead && totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
            {
                totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed);
            }

            return; 
        }
   
        if (value.isPressed && IsGrounded() && !heartsHealthVisual.isDead)
        {
            totoRigidbody.velocity = new Vector2(0f, totoJumpSpeed); 
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
        if (heartsHealthVisual.isDead) { return; }

        if (value.isPressed)
        {
            Invoke("InstantiateProjectile", 0.5f);
            //InstantiateProjectile();
            totoAnimator.SetTrigger("Attack");
        }
    }

    private void InstantiateProjectile()
    {
        Instantiate(projectile, catapultPosition.position, transform.rotation);
    }



    //****************
    // Setting Animations
   
    //**************

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlimeEnemy") && !heartsHealthVisual.isDead)
        {
            totoAnimator.SetTrigger("isHit");
            Debug.Log("Chot lagi");
        }

        if (heartsHealthVisual.isDead && collision.CompareTag("SlimeEnemy"))
        {
            Debug.Log("maar gaya");
            totoAnimator.SetTrigger("Dying");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            DamageKnockBack(damageAmount);
            totoAnimator.SetTrigger("isHit");
            Debug.Log("Spike hits");
        }

        if (heartsHealthVisual.isDead && totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            Debug.Log("Spikes se maar gaya");
            totoAnimator.SetTrigger("Dying");
        }
    }





}
