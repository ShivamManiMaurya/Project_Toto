using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
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
    }

    public void CheckPlayerLife(int damageAmount)
    {
        if (playerLives > 1)
        {
            TakeLife(damageAmount);
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    private void TakeLife(int damageAmount)
    {
        playerLives -= damageAmount;
        _livesText.text = playerLives.ToString();
    }

}
