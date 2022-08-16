using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    [SerializeField] float ratSpeed = 5f, health, maxHealth = 1f;
    [SerializeField] bool mustPetrol;
    [SerializeField] LayerMask platfromLayer;
    [SerializeField] private int _ratKillPoints = 500;
    [SerializeField] private AudioClip _ratDeathSfx;
    [SerializeField] private AudioClip[] clips;

    private int clipIndex;
    private float ratScale;
    private bool mustTurn;

    BoxCollider2D ratBoxCollider;
    Transform     ratTransform;
    Rigidbody2D   ratRigidbody;
    Animator      ratAnimator;
    Transform groundIsPresentOrNot;
    AudioSource audioSource;
    

    void Start()
    {
        ratBoxCollider = GetComponent<BoxCollider2D>();
        ratTransform = GetComponent<Transform>();
        ratRigidbody = GetComponent<Rigidbody2D>();
        groundIsPresentOrNot = transform.Find("RatGroundIsPresentOrNot");
        ratAnimator = GetComponent<Animator>();
        ratScale = ratTransform.localScale.x;
        audioSource = GetComponent<AudioSource>();

        mustPetrol = true;
        health = maxHealth;
    }

    void Update()
    {
        if (mustPetrol)
        { Patrol(); }

        if (!audioSource.isPlaying)
        {
            clipIndex = Random.Range(0, clips.Length - 1);
            audioSource.clip = clips[clipIndex];
            audioSource.PlayDelayed(Random.Range(1f, 10f));
            Debug.Log("chu chu is playing");
        }


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

        if (health > 0)
        {
            ratRigidbody.velocity = new Vector2(ratSpeed * Time.fixedDeltaTime, ratRigidbody.velocity.y);
        }
        else
        {
            ratRigidbody.velocity = new Vector2(0f, ratRigidbody.velocity.y);
        }
    }

    void Flip()
    {
        ratScale = -ratScale;
        ratSpeed = -ratSpeed;
        ratTransform.localScale = new Vector2(ratScale, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ratBoxCollider.IsTouchingLayers(platfromLayer))
        {
            Flip();
        }
    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        //SoundManager.Instance.PlaySound(_ratHitSfx, 1f);
        Debug.Log("rathit");

        if (health <= 0)
        {
            SoundManager.Instance.PlaySound(_ratDeathSfx, 1f);
            FindObjectOfType<GameSession>().ScoreUpdater(_ratKillPoints);
            ratAnimator.SetTrigger("Dying");
            Destroy(gameObject, 1f);
        }

    }



}
