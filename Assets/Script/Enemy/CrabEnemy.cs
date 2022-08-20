using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemy : MonoBehaviour
{
    [SerializeField] float crabSpeed = 5f, health, maxHealth = 3f;
    [SerializeField] bool mustPetrol;
    [SerializeField] LayerMask platfromLayer;
    [SerializeField] private int _crabKillPoints = 200;
    [SerializeField] private AudioClip _crabHitSfx, _crabDeathSfx;

    private float crabScale;
    private bool mustTurn;

    BoxCollider2D crabBoxCollider;
    Transform     crabTransform;
    Rigidbody2D   crabRigidbody;
    Transform groundIsPresentOrNot;
    Animator crabAnimator;

    void Start()
    {
        crabBoxCollider = GetComponent<BoxCollider2D>();
        crabTransform = GetComponent<Transform>();
        crabRigidbody = GetComponent<Rigidbody2D>();
        groundIsPresentOrNot = transform.Find("CrabGroundIsPresentOrNot");
        crabAnimator = GetComponent<Animator>();
        crabScale = crabTransform.localScale.x;

        mustPetrol = true;
        health = maxHealth;
    }

    void Update()
    {
        if (mustPetrol)
            Patrol();
    }

    // For handling the physics better FixedUpdate() is used
    private void FixedUpdate()
    {
        if (mustPetrol)
            mustTurn = !Physics2D.OverlapCircle(groundIsPresentOrNot.position, 0.1f, platfromLayer);
    }

    // petrolling the area untill ground is present
    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }

        if (health > 0)
        {
            crabRigidbody.velocity = new Vector2(crabSpeed * Time.fixedDeltaTime, crabRigidbody.velocity.y);
        }
        else
        {
            crabRigidbody.velocity = new Vector2(0f, crabRigidbody.velocity.y);
        }
    }

    void Flip()
    {
        crabScale = -crabScale;
        crabSpeed = -crabSpeed;
        crabTransform.localScale = new Vector2(crabScale, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (crabBoxCollider.IsTouchingLayers(platfromLayer))
        {
            Flip();
        }
    }

    // Crab Got hit
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        crabAnimator.SetTrigger("GotHit");
        SoundManager.Instance.PlaySound(_crabHitSfx, 1f);

        if (health <= 0)
        {
            SoundManager.Instance.PlaySound(_crabDeathSfx, 1f);
            FindObjectOfType<GameSession>().ScoreUpdater(_crabKillPoints);
            crabAnimator.SetTrigger("Dying");
            Destroy(gameObject, 1f);
        }
        
    }

}
