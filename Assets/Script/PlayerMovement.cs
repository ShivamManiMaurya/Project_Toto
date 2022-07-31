using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float totoSpeed = 5;

    Vector2 moveInput;
    Rigidbody2D totoRigidbody;

    void Start()
    {
        totoRigidbody = GetComponent<Rigidbody2D>();
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
        Debug.Log(totoRigidbody.velocity);

        bool val = totoRigidbody.velocity.x > 0;
        Debug.Log(val);

        if (totoRigidbody.velocity.x >= 0)
        {
            totoRigidbody.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            totoRigidbody.transform.localScale = new Vector3(-1, 1, 1);
        }
    }


}
