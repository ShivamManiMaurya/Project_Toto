using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemy : MonoBehaviour
{
    [SerializeField] float crabSpeed = 5f, health, maxHealth = 3f;
    [SerializeField] bool mustPetrol;
    [SerializeField] LayerMask platfromLayer;

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
        crabRigidbody.velocity = new Vector2(crabSpeed * Time.fixedDeltaTime, crabRigidbody.velocity.y);
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


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

}
