using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    HeartsHealthVisual heartsHealthVisual;
    int numOfGameSession;
    public int gameCheck = 3;

    void Awake()
    {
        numOfGameSession = FindObjectsOfType<GameSession>().Length;
        
        if (numOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log(numOfGameSession);
    }



    private void Start()
    {
        heartsHealthVisual = FindObjectOfType<HeartsHealthVisual>();
    }

    private void Update()
    {
        //Debug.Log("Update = " + numOfGameSession);
    }

    public void CheckPlayerLife()
    {
        if (heartsHealthVisual.isDead)
        {
            ResetGameSession();
        }
        else
        {
            //DontDestroyOnLoad(gameObject);
            gameCheck--;
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

}
