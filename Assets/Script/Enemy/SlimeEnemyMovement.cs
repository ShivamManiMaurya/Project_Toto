using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    //[SerializeField] int damageAmount = 1;
    [SerializeField] float slimeSpeed = 5f;
    [SerializeField] bool mustPetrol;
    [SerializeField] LayerMask platfromLayer;

    private float slimeScale;
    private bool mustTurn;

    BoxCollider2D slimeBoxCollider;
    Transform slimeTransform;
    Rigidbody2D slimeRigidbody;
    Transform groundIsPresentOrNot;
    Animator slimeAnimator;
    

    void Start()
    {
        slimeBoxCollider = GetComponent<BoxCollider2D>();
        slimeTransform = GetComponent<Transform>();
        slimeScale = slimeTransform.localScale.x;
        slimeRigidbody = GetComponent<Rigidbody2D>();
        groundIsPresentOrNot = transform.Find("GroundIsPresentOrNot");
        slimeAnimator = GetComponent<Animator>();

        mustPetrol = true;
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
        if (mustTurn )
        {
            Flip();
        }
        slimeRigidbody.velocity = new Vector2(slimeSpeed  * Time.fixedDeltaTime, slimeRigidbody.velocity.y);
    }

    void Flip()
    {
        slimeScale = -slimeScale;
        slimeSpeed = -slimeSpeed;
        slimeTransform.localScale = new Vector2(slimeScale, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Fliping the enemey when they triggered with the trigger collider
        if (slimeBoxCollider.IsTouchingLayers(platfromLayer))
        {
            Flip();
        }
    }

    public void slimeDeathAnimation()
    {
        slimeAnimator.SetTrigger("SlimeDying");
    }
}
