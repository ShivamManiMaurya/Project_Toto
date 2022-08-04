using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;

    Rigidbody2D projectileRigidbody;
    PlayerMovement player;
    float projectileVelocity;

    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        projectileVelocity = player.transform.localScale.x * projectileSpeed;
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
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        Invoke("DestroyProjectileHittingGround", 0.5f);

    }

    private void DestroyProjectileHittingGround()
    {
        if (projectileRigidbody.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            Destroy(gameObject);
        }
    }



}
