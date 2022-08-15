using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private AudioClip _uiButtonPressedClip, _buttonHover, _stageOneMusic;
    

    public void PlayGame()
    {
        StartCoroutine(LoadStageOne());
    }

    IEnumerator LoadStageOne()
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSecondsRealtime(_waitTime);

        //Load the stage
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }

        if (SceneManager.GetActiveScene().name == "You_Won")
        {
            SceneManager.LoadScene("Stage_1");
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

        SoundManager.Instance.PlayMusic(_stageOneMusic);
    }

    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }

    public void PlayButtonClip()
    {
        SoundManager.Instance.PlaySound(_uiButtonPressedClip, 1f);
    }

    public void PlayHoverClip()
    {
        SoundManager.Instance.PlaySound(_buttonHover, 1f);
    }

}
