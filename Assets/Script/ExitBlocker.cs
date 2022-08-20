using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlocker : MonoBehaviour
{
    BossHealth bossHealth;

    private void Start()
    {
        bossHealth = FindObjectOfType<BossHealth>();
    }

    private void Update()
    {
        if (bossHealth.bossIsDead)
        {
            Destroy(gameObject);
        }
    }
}
