using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextStage : MonoBehaviour
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
