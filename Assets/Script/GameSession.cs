using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 7;
    [SerializeField] public int playerScore = 0;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AudioClip _gameOverMusic;
    

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
        if (SceneManager.GetActiveScene().name == "You_Won")
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

        SceneManager.LoadScene("Game_Over_Die");
        SoundManager.Instance.PlayMusic(_gameOverMusic);

        Destroy(gameObject);
    }

    private void TakeLife(int damageAmount)
    {
        playerLives -= damageAmount;

        // This if else condition used because if playerLives is less than 0 then it shows on the screen
        if (playerLives < 0)
        {
            _livesText.text = "0";
        }
        else
        {
            _livesText.text = playerLives.ToString();
        }
    }

    public void GiveLife(int healthAmount)
    {
        if (playerLives > 0 && playerLives < 9)
        {
            playerLives += healthAmount;
            _livesText.text = playerLives.ToString();
        }
    }
        
    public void ScoreUpdater(int pointsScored)
    {
        playerScore += pointsScored;
        _scoreText.text = playerScore.ToString();
    }



}
