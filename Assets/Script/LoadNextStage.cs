using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextStage : MonoBehaviour
{
    [SerializeField] private float _waitTime = 0.5f;
    [SerializeField] private AudioClip _changeAudioTrack, _youWonMusic, _bossMusic, _levelFiveMusic;

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

        if (SceneManager.GetActiveScene().name == "Stage_6")
        {
            Debug.Log("YOu won level music section");
            SoundManager.Instance.PlayMusic(_youWonMusic);
        }
        else if (SceneManager.GetActiveScene().name == "Stage_4")
        {
            SoundManager.Instance.PlayMusic(_levelFiveMusic);
        }
        else if (SceneManager.GetActiveScene().name == "Stage_5")
        {
            SoundManager.Instance.PlayMusic(_bossMusic);
        }
        else
        {
            SoundManager.Instance.PlayMusic(_changeAudioTrack);
        }
        
    }
}
