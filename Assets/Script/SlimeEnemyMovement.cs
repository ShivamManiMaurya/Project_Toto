using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] float slimeSpeed = 5f;
    [SerializeField] bool mustPetrol;
    [SerializeField] LayerMask platfromLayer;

    float slimeScale;
    bool mustTurn;

    BoxCollider2D slimeBoxCollider;
    Transform slimeTransform;
    Rigidbody2D slimeRigidbody;
    Transform groundIsPresentOrNot;
    
    
    

    void Start()
    {
        slimeBoxCollider = GetComponent<BoxCollider2D>();
        slimeTransform = GetComponent<Transform>();
        slimeScale = slimeTransform.localScale.x;
        slimeRigidbody = GetComponent<Rigidbody2D>();
        groundIsPresentOrNot = transform.Find("GroundIsPresentOrNot");

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
        PlayerMovement player = collider.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // We hit the player
            //Vector2 knockbackDir = (player.totoRigidbody.position - transform.position).normalized;
            player.DamageKnockBack(damageAmount);
            Debug.Log("hit");
        }


        if (slimeBoxCollider.IsTouchingLayers(platfromLayer))
        {
            Flip();
            
        }
    }


}
