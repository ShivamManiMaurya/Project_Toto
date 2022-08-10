using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private int ptsScored = 100;
    [SerializeField] private AudioClip _coinPickup;
    [SerializeField] private float volume = 0.5f;

    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !wasCollected)
        {
            SoundManager.Instance.PlaySound(_coinPickup, volume);
            

            wasCollected = true;   
            FindObjectOfType<GameSession>().ScoreUpdater(ptsScored);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
