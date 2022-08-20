using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterMoveToStart : MonoBehaviour
{
    [SerializeField] private float _waitTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(NextStage());
        }
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(_waitTime);

        if (SceneManager.GetActiveScene().name == "Stage_5")
        {
            SceneManager.LoadScene("Stage_5");
        }

    }

}
