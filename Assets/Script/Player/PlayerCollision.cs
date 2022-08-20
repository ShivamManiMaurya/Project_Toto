using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _playerHitSfx, _playerDeathSfx;
    [SerializeField] private int _dangerLayerDamageAmount = 1;
    [SerializeField] private int _slimeEnemyDamageAmount = 1;
    [SerializeField] private int _crabEnemyDamageAmount = 2, _ratEnemyDamageAmt = 1;

    CapsuleCollider2D totoBodyCollider;
    BoxCollider2D totoFeetCollider;
    Animator totoAnimator;
    GameSession gameSession;

    public bool totoIsDead = false;

    private void Start()
    {
        totoBodyCollider = GetComponent<CapsuleCollider2D>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
        totoAnimator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // for slime trigger
        if (collision.CompareTag("SlimeEnemy") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_slimeEnemyDamageAmount);
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // for danger collision like spikes, lava etc
        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            TotoGotHit(_dangerLayerDamageAmount);
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
        }

        // for crab collision
        if (collision.gameObject.CompareTag("CrabEnemy") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_crabEnemyDamageAmount);
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
        }

        // for rat collision
        if (collision.gameObject.CompareTag("RatEnemy") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_ratEnemyDamageAmt);
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
        }

        // for Boss collision
        if (collision.gameObject.CompareTag("Boss") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_ratEnemyDamageAmt);
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
        }

    }


    public void TotoGotHit(int damageAmount)
    {
        gameSession.CheckPlayerLife(damageAmount);
        totoAnimator.SetTrigger("isHit");

        if (gameSession.playerLives <= 0)
        {
            totoIsDead = true;
            totoAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(_playerDeathSfx, Camera.main.transform.position);
            // for going to the game over scene after death use this
            gameSession.CheckPlayerLife(0);
        }
    }


}
