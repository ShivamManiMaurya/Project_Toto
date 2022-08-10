using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthAmount = 1;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().GiveLife(_healthAmount);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
