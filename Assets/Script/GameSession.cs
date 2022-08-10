using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    [SerializeField] public int playerScore = 0;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    

    private void Awake()
    {
        int numOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSession > 1 )
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _livesText.text = playerLives.ToString();
        _scoreText.text = playerScore.ToString();
    }

    private void Update()
    {
        if (SceneManager.sceneCountInBuildSettings == (SceneManager.GetActiveScene().buildIndex + 1))
        {
            Destroy(gameObject);
        }
    }

    public void CheckPlayerLife(int damageAmount)
    {
        if (playerLives > 0)
        {
            TakeLife(damageAmount);
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(3);

        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    private void TakeLife(int damageAmount)
    {
        playerLives -= damageAmount;
        _livesText.text = playerLives.ToString();
    }
        
    public void ScoreUpdater(int pointsScored)
    {
        playerScore += pointsScored;
        _scoreText.text = playerScore.ToString();
    }

}
