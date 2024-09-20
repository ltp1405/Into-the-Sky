using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject mainMenu;
    private vThirdPersonInput vThirdPersonInput;
    private vThirdPersonController vThirdPersonController;
    public BackgroundMusicManager musicManager;
    public PlayerSaveManager saveManager;

    void Awake()
    {
        vThirdPersonInput = player.GetComponent<vThirdPersonInput>();
        vThirdPersonController = player.GetComponent<vThirdPersonController>();
    }

    void Start()
    {
        StartMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartMenu();
        }
    }

    public void EnablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = true;
    }

    public void DisablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = false;
        vThirdPersonController.StopRunningSound(); 
    }

    public void StartMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DisablePlayerBehaviours();
        mainMenu.SetActive(true);
        saveManager.SaveInfo();
        Time.timeScale = 0;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EnablePlayerBehaviours();
        mainMenu.SetActive(false);
        saveManager.LoadInfo();
        musicManager.RestartBackgroundMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
