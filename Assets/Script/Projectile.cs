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
            FindObjectOfType<SlimeEnemyMovement>().slimeDeathAnimation();

            Destroy(collision.gameObject, 1f);
            
            
            SoundManager.Instance.PlaySound(_slimeDeathSfx, _slimeDeathVol);
            FindObjectOfType<GameSession>().ScoreUpdater(_slimeKillPoints);

            Destroy(gameObject);
        }

        Invoke("DestroyProjectileHittingGround", 0.5f);

    }

    private void SlimeDeathDelay(GameObject colGameObject)
    {

        // here collision.gameObject is the slime enemy gameObject
        Destroy(colGameObject);

        Debug.Log("slimedeathdelay working");
    }

    private void DestroyProjectileHittingGround()
    {
        if (projectileRigidbody.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            Destroy(gameObject);
        }
    }



}
