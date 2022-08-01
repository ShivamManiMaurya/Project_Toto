using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float totoSpeed = 5;

    bool facingRight = true;

    Vector2 moveInput;
    Rigidbody2D totoRigidbody;
    Animator totoAnimator;
    

    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
        totoAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
    }

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


}
