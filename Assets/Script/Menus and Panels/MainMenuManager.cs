using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float waitTime = 2f;

    public void PlayGame()
    {
        StartCoroutine(LoadStageOne());
    }

    IEnumerator LoadStageOne()
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(waitTime);

        //Load the stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
