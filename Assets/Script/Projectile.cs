using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] private AudioClip _slimeDeathSfx;
    [SerializeField] private float _slimeDeathVol = 1f;
    [SerializeField] private int _slimeKillPoints = 100;
    

    Rigidbody2D projectileRigidbody;
    PlayerMovement player;
    float projectileVelocity;
    CrabEnemy crabEnemy;
    RatEnemy ratEnemy;
    BossHealth bossHealth;

    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        projectileVelocity = player.transform.localScale.x * projectileSpeed;
        crabEnemy = FindObjectOfType<CrabEnemy>();
        ratEnemy = FindObjectOfType<RatEnemy>();
        bossHealth = FindObjectOfType<BossHealth>();
    }

    void Update()
    {
        ProjectileShoot();
    }

    private void ProjectileShoot()
    {
        projectileRigidbody.velocity = new Vector2(projectileVelocity, projectileRigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeEnemy"))
        {
            FindObjectOfType<SlimeEnemyMovement>().slimeDeathAnimation();
            Destroy(collision.gameObject, 1f);
            
            SoundManager.Instance.PlaySound(_slimeDeathSfx, _slimeDeathVol);
            FindObjectOfType<GameSession>().ScoreUpdater(_slimeKillPoints);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("CrabEnemy"))
        {
            crabEnemy.TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("RatEnemy"))
        {
            ratEnemy.TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }


        DestroyProjectileHittingGround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) 
        {
            bossHealth.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void DestroyProjectileHittingGround()
    {
        if (projectileRigidbody.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            Destroy(gameObject);
        }
    }



}
