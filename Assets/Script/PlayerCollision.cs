using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _playerHitSfx, _playerDeathSfx;

    [SerializeField] private int _dangerLayerDamageAmount = 1;
    [SerializeField] private int _slimeEnemyDamageAmount = 1;
    [SerializeField] private int _crabEnemyDamageAmount = 2;

    CapsuleCollider2D totoBodyCollider;
    BoxCollider2D totoFeetCollider;
    Animator totoAnimator;
    GameSession gameSession;

    private void Start()
    {
        totoBodyCollider = GetComponent<CapsuleCollider2D>();
        totoFeetCollider = GetComponent<BoxCollider2D>();
        totoAnimator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlimeEnemy") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_slimeEnemyDamageAmount);
            totoAnimator.SetTrigger("isHit");
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
            Debug.Log("Chot lagi");
        }

        if ((gameSession.playerLives <= 0) && collision.CompareTag("SlimeEnemy"))
        {
            Debug.Log("maar gaya");
            totoAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(_playerDeathSfx, Camera.main.transform.position);
            // for going to the first level after death use this
            gameSession.CheckPlayerLife(0);
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // for danger collision like spikes, lava etc
        if (totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            //DamageKnockBack(_dangerLayerDamageAmount);
            TotoGotHit(_dangerLayerDamageAmount);
            totoAnimator.SetTrigger("isHit");
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
            Debug.Log("Spike hits");
        }

        if ((gameSession.playerLives <= 0) && totoFeetCollider.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            Debug.Log("Spikes se maar gaya");
            totoAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(_playerDeathSfx, Camera.main.transform.position);
            gameSession.CheckPlayerLife(0);
        }


        // for crab collision
        if (collision.gameObject.CompareTag("CrabEnemy") && (gameSession.playerLives > 0))
        {
            TotoGotHit(_crabEnemyDamageAmount);
            totoAnimator.SetTrigger("isHit");
            AudioSource.PlayClipAtPoint(_playerHitSfx, Camera.main.transform.position);
            Debug.Log("Chot lagi");
        }

        if ((gameSession.playerLives <= 0) && collision.gameObject.CompareTag("CrabEnemy"))
        {
            Debug.Log("maar gaya");
            totoAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(_playerDeathSfx, Camera.main.transform.position);
            // for going to the first level after death use this
            gameSession.CheckPlayerLife(0);
        }
    }



    public void TotoGotHit(int damageAmount)
    {
        gameSession.CheckPlayerLife(damageAmount);
    }






}
