using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthAmount = 1;
    [SerializeField] private AudioClip _healthSfx;
    [SerializeField] private float _healthSfxVolume = 1f;


    private GameSession gameSession;
    bool wasCollected = false;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !wasCollected && gameSession.playerLives < 9)
        {
            SoundManager.Instance.PlaySound(_healthSfx, _healthSfxVolume);

            wasCollected = true;
            FindObjectOfType<GameSession>().GiveLife(_healthAmount);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
