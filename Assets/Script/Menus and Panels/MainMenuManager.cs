using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private AudioClip _uiButtonPressedClip, _buttonHover;
    

    public void PlayGame()
    {
        StartCoroutine(LoadStageOne());
    }

    IEnumerator LoadStageOne()
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(_waitTime);

        //Load the stage
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }

    public void PlayButtonClip()
    {
        SoundManager.Instance.PlaySound(_uiButtonPressedClip);
    }

    public void PlayHoverClip()
    {
        SoundManager.Instance.PlaySound(_buttonHover);
    }

}
