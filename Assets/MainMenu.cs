using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject mainMenu;
    private vThirdPersonInput vThirdPersonInput;
    private vThirdPersonController vThirdPersonController;
    public BackgroundMusicManager musicManager;

    void Awake()
    {
        vThirdPersonInput = player.GetComponent<vThirdPersonInput>();
        vThirdPersonController = player.GetComponent<vThirdPersonController>();
    }

    public void EnablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = true;
        vThirdPersonController.enabled = true; 
    }

    void DisablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = false;
        vThirdPersonController.enabled = false; 
        vThirdPersonController.StopRunningSound(); 
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DisablePlayerBehaviours();
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        EnablePlayerBehaviours();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        musicManager.RestartBackgroundMusic();
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Playground++");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
