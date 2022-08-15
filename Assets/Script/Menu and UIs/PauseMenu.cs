using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // this is a input controller which is accedentlly typed Toto
    private Toto playerControl;
    private InputAction menu;
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        playerControl = new Toto();
    }

    

    private void OnEnable()
    {
        menu = playerControl.Menu.Escape;
        menu.Enable();


        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }

        
    }

    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 
    }

    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


}
