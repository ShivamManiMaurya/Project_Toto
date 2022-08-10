using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerMovement player = collision.GetComponent<PlayerMovement>();
        //if (player != null)
        //{
        //    // we heal the player
        //    player.Heal(healAmount);
        //    Destroy(gameObject);
        //}
    }

}
